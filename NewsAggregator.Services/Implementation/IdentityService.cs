using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Data.Models.Identity;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Options;
using NewsAggregator.Services.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ApplicationDbContext _context;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            ApplicationDbContext context
        )
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
        }

        public async Task<AuthenticationResult> LoginAsync(string name, string password)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Result = AuthenticationResultType.UserNotFound,
                    Errors = new[] { "User does not exist" }
                };
            }

            var userProvidedValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userProvidedValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Wrong name/password combination" },
                    Result = AuthenticationResultType.WrongCombination,
                };
            }

            return await GenerateAuthResult(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(string name, string password)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                return new AuthenticationResult
                {
                    Result = AuthenticationResultType.UserAlreadyExists,
                    Errors = new[] { "user with given name already exists" },
                };
            }

            var newUser = new ApplicationUser { UserName = name };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Result = AuthenticationResultType.RegisterSuccess,
                    Errors = createdUser.Errors.Select(x => x.Description),
                };
            }

            // add userid to applicationUserSettings table
            // I think it's a good practice to decouple identity generated table from application specific tables.
            _context.ApplicationUserSettings.Add(new ApplicationUserSettings { User = newUser });
            await _context.SaveChangesAsync();

            return await GenerateAuthResult(newUser);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);

            if (principal == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid token" } };
            }

            var expiryDateUnixTimeStamp = long.Parse(
                principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value
            );

            var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(
                expiryDateUnixTimeStamp
            );

            // if (expiryDateUtc > DateTime.UtcNow)
            // {
            //     return new AuthenticationResult
            //     {
            //         Errors = new[]
            //         {
            //             "This token hasn't expired yet"
            //         }
            //     };
            // }

            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var refreshTokenFromDB = _context.RefreshTokens.SingleOrDefault(
                x => x.Token == refreshToken
            );

            if (refreshTokenFromDB == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token doesn't exist" }
                };
            }

            if (DateTime.UtcNow > refreshTokenFromDB.ExpiryDate)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has expired" }
                };
            }

            if (refreshTokenFromDB.Invalidated)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has been invalidated" }
                };
            }

            if (refreshTokenFromDB.Used)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has been used" }
                };
            }

            if (refreshTokenFromDB.JwtId != jti)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token doesnt match this JWT" }
                };
            }

            refreshTokenFromDB.Used = true;
            _context.RefreshTokens.Update(refreshTokenFromDB);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(
                principal.Claims.Single(x => x.Type == "UserId").Value
            );

            return await GenerateAuthResult(user);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            try
            {
                // todo simple workaround of lifetime problem, need to figure more elegant way
                // if validatelifetime is set to true before validating a token there is a high possibility that
                // the token we want to refresh is expired (actually who would want to refresh not expired token)
                _tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(
                    token,
                    _tokenValidationParameters,
                    out SecurityToken validatedToken
                );
                _tokenValidationParameters.ValidateLifetime = true;
                if (!IsValidAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private bool IsValidAlgorithm(SecurityToken validateToken)
        {
            if (validateToken is JwtSecurityToken jwtSecurityToken)
            {
                if (
                    jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase
                    )
                )
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<AuthenticationResult> GenerateAuthResult(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserId", user.Id)
                    }
                ),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                // todo move expirydate to get from appsettings
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Result = AuthenticationResultType.LoginSuccess,
                Token = tokenHandler.WriteToken(token),
                //this token is GUID from sql server, because I added a valueGeneratedOnAdd()
                RefreshToken = refreshToken.Token
            };
        }
    }
}

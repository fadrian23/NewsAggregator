using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.Models.Identity;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<UserServiceResult> ChangeNameAsync(string newUsername, string userId)
        {
            var user = await GetUserAsync(userId);

            var result = await _userManager.SetUserNameAsync(user, newUsername);
            if (result.Succeeded)
            {
                return new UserServiceResult { Success = true };
            }

            return new UserServiceResult
            {
                Success = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        public async Task<UserServiceResult> ChangePasswordAsync(
            string oldPassword,
            string newPassword,
            string userId
        )
        {
            var user = await GetUserAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                return new UserServiceResult { Success = true, };
            }
            return new UserServiceResult
            {
                Success = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        private async Task<ApplicationUser> GetUserAsync(string userId) =>
            await _userManager.FindByIdAsync(userId);
    }
}

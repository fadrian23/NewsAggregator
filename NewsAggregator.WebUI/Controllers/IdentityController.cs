using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models.Requests;
using NewsAggregator.WebUI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new FailedAuthResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors).Select(z => z.ErrorMessage)
                });
            }
            var authenticationResponse = await _identityService.RegisterAsync(request.Name, request.Password);
            return Result(authenticationResponse);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var authenticationResponse = await _identityService.LoginAsync(request.Name, request.Password);

            return Result(authenticationResponse);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(UserRefreshRequest request)
        {
            var authenticationResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);
            return Result(authenticationResponse);

        }

        private IActionResult Result(AuthenticationResult authenticationResponse)
        {
            if (authenticationResponse.Result == AuthenticationResultType.WrongCombination)
            {
                return BadRequest(new FailedAuthResponse
                {
                    Errors = authenticationResponse.Errors
                });
            }

            if(authenticationResponse.Result == AuthenticationResultType.UserNotFound) 
            {
                return NotFound( new FailedAuthResponse 
                {
                    Errors = authenticationResponse.Errors
                });
            }

            return Ok(new SuccessfulAuthResponse
            {
                Token = authenticationResponse.Token,
                RefreshToken = authenticationResponse.RefreshToken
            });
        }
    }
}

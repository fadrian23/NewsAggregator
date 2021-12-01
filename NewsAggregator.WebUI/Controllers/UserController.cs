using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Extensions;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeName([FromBody] UserChangeNameRequest userChangeNameRequest)
        {
            if (userChangeNameRequest.UserName == null)
            {
                return BadRequest("null username");
            }
            var result = await _userService.ChangeNameAsync(userChangeNameRequest.UserName, User.GetUserId());
            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new UserChangeNameRequest
            {
                UserName = userChangeNameRequest.UserName
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordRequest userChangePasswordRequest)
        {
            var result = await _userService.ChangePasswordAsync(userChangePasswordRequest.oldPassword, userChangePasswordRequest.newPassword, User.GetUserId());
            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }



    }
}

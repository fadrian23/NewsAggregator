using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Services;
using NewsAggregator.Services.Extensions;
using System;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IRssSitesService _rssSiteService;

        public PostsController(IRssSitesService rssSiteService)
        {
            _rssSiteService = rssSiteService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("saveforlater")]
        public IActionResult SavePostForLater([FromQuery] int postId)
        {
            var userId = User.GetUserId();

            var result = _rssSiteService.SavePostForLater(userId, postId);

            return result ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("removeforlater")]
        public IActionResult RemovePostForLater([FromQuery] int postId)
        {
            var userId = User.GetUserId();

            var result = _rssSiteService.RemovePostForLater(userId, postId);

            return result ? Ok(result) : BadRequest(result);
        }

    }
}

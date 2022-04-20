using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Extensions;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Services;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IArticlesService _articlesService;

        public PostsController(IArticlesService articlesService)
        {
            _articlesService = articlesService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("saveforlater")]
        public IActionResult SavePostForLater([FromQuery] int postId)
        {
            var userId = User.GetUserId();

            var result = _articlesService.SavePostForLater(userId, postId);

            return result ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("removeforlater")]
        public IActionResult RemovePostForLater([FromQuery] int postId)
        {
            var userId = User.GetUserId();

            var result = _articlesService.RemovePostForLater(userId, postId);

            return result ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("getpostsforlater")]
        public IActionResult GetPostsForLater([FromQuery] PaginationFilter paginationFilter)
        {
            var userId = User.GetUserId();

            var result = _articlesService.GetPostsByDateRangeForLater(paginationFilter, userId);

            return Ok(result);
        }
    }
}

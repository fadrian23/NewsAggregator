using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models.Requests;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Helpers;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RedditController : ControllerBase
    {
        private readonly ISiteService _redditService;
        private readonly ILogger<RedditController> _logger;

        public RedditController(ISiteFactory siteFactory, ILogger<RedditController> logger)
        {
            _redditService = siteFactory.For(AvailableSites.Reddit);
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Posts([FromQuery] PaginationFilter paginationFilter)
        {
            var filter = new PaginationFilter
            {
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize
            };

            System.Console.WriteLine($"Pagenumber: {paginationFilter.PageNumber}, PageSize: {paginationFilter.PageSize}");
            var posts = _redditService.GetPosts(filter);
            return Ok(posts);

        }

        [HttpGet]
        public IActionResult GetPostsByDate([FromBody] GetPostsByDateRequest getPostsByDateRequest)
        {
            _logger.LogInformation($"Getting reddit posts by date: {getPostsByDateRequest.Date}");

            var posts = _redditService.GetPostsByDate(getPostsByDateRequest.Date);

            return posts != null ? Ok(posts) : NotFound();
        }
    }

}

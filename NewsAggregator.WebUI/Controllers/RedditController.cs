using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models.Requests;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RedditController : ControllerBase
    {
        private readonly IRedditService _redditService;
        private readonly ILogger<RedditController> _logger;

        public RedditController(IRedditService redditService, ILogger<RedditController> logger)
        {
            _redditService = redditService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Posts()
        {
            _logger.LogInformation("getting reddit posts");
            var posts = _redditService.GetPosts();
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

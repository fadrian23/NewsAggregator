﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models.Requests;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;
        private readonly ILogger<HackerNewsController> _logger;


        public HackerNewsController(IHackerNewsService hackerNewsService, ILogger<HackerNewsController> logger)
        {
            _hackerNewsService = hackerNewsService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Posts()
        {
            _logger.LogInformation("GET all posts from HN");

            var posts = _hackerNewsService.GetPosts();

            return Ok(posts);
        }


        [HttpGet]
        public IActionResult GetPostsByDate([FromBody] GetPostsByDateRequest getPostsByDateRequest)
        {
            _logger.LogInformation($"GET HN posts by date {getPostsByDateRequest.Date}");

            var posts = _hackerNewsService.GetPostsByDate(getPostsByDateRequest.Date);

            return posts != null ? Ok(posts) : NotFound();
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Extensions;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models;
using NewsAggregator.WebUI.Models.Requests;
using NewsAggregator.WebUI.Models.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeedsController : ControllerBase
    {
        private readonly ISitesService _sitesService;

        public FeedsController(ISitesService sitesService)
        {
            _sitesService = sitesService;
        }

        [HttpGet]
        [Route("getarticles")]
        public IActionResult GetArticlesFromSubscribedFeeds(
            [FromQuery] PaginationFilter paginationFilter,
            DateRange dateRange
        )
        {
            string userId = User.GetUserId();

            var posts = _sitesService.GetArticlesFromSubscribedFeeds(
                userId,
                paginationFilter,
                dateRange.Start,
                dateRange.End
            );

            Console.WriteLine(dateRange.Start);
            Console.WriteLine(dateRange.End);

            if (posts == null)
            {
                return NoContent();
            }

            return Ok(posts);
        }

        [HttpPost]
        [Route("subscribe")]
        public IActionResult SubscribeToFeeds(IEnumerable<int> feedIds)
        {
            string userId = User.GetUserId();

            var result = _sitesService.SubscribeToFeeds(feedIds, userId);

            var response = new UserSitesSubscribeResponse
            {
                Errors = result.Errors,
                Success = result.Success
            };

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [Route("getsubscribedfeeds")]
        public IActionResult GetSubscribedFeeds()
        {
            string userId = User.GetUserId();

            var result = _sitesService.GetSubscribedFeeds(userId);

            return Ok(result);
        }

        [HttpGet]
        [Route("available")]
        [AllowAnonymous]
        public IActionResult GetAvailableFeeds()
        {
            var availableFeeds = _sitesService.GetAvailableFeeds();

            return Ok(availableFeeds);
        }
    }
}

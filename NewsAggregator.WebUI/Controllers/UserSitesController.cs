using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Extensions;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models.Requests;
using NewsAggregator.WebUI.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserSitesController : ControllerBase
    {
        private readonly ISitesService _sitesService;

        public UserSitesController(ISitesService sitesService)
        {
            _sitesService = sitesService;
        }


        [HttpGet]
        [Route("getpostsbydaterange")]
        public IActionResult GetPostsByDateFromSubscribedSites([FromQuery] PaginationFilter paginationFilter, DateTime startDate, DateTime endDate)
        {
            string userId = User.GetUserId();

            var posts = _sitesService.GetPostsFromUserSites(userId, paginationFilter, startDate, endDate);

            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        [HttpPost]
        [Route("subscribe")]
        public IActionResult SubscribeToSites(UserSitesSubscribeRequest userSitesSubscribeRequest)
        {
            string userId = User.GetUserId();

            if (userSitesSubscribeRequest.Sites == null)
            {
                return BadRequest();
            }

            var result = _sitesService.SubscribeToSites(userSitesSubscribeRequest.Sites, userId);

            var response = new UserSitesSubscribeResponse
            {
                Errors = result.Errors,
                Success = result.Success
            };


            return response.Success ? Ok() : BadRequest(response);
        }

        [HttpGet]
        [Route("getsubscribedsites")]
        public IActionResult GetSubscribedSites()
        {
            string userId = User.GetUserId();

            var result = _sitesService.GetSubscribedSites(userId);

            return Ok(result);
        }

        [HttpGet]
        [Route("available")]
        public IActionResult GetAvailableSites()
        {
            var availableSites = AvailableRssFeeds.RssFeeds.Keys.ToArray();

            return Ok(availableSites);
        }
    }


}

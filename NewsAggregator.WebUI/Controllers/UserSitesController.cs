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
        [Route("posts")]
        public IActionResult GetPostsFromSubscribedSites([FromQuery] PaginationFilter paginationFilter)
        {
            string userId = User.GetUserId();

            var posts = _sitesService.GetPostsFromUserSites(userId, paginationFilter);

            if (posts == null)
            {
                return NotFound();
            }

            string jsonWithoutNulls = JsonConvert.SerializeObject(posts, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return Content(jsonWithoutNulls, "application/json");
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
        [Route("available")]
        public IActionResult GetAvailableSites()
        {
            var availableSites = AvailableRssFeeds.RssFeeds.Keys.ToArray();

            return Ok(availableSites);
        }
    }


}

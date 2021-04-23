using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Extensions;
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
    [Authorize]
    public class UserSitesController : ControllerBase
    {
        private readonly ISitesService _sitesService;

        public UserSitesController(ISitesService sitesService)
        {
            _sitesService = sitesService;
        }


        //
        // not sure if want to leave it like here
        // maybe return links to controllers and make multiple requests from frontend
        // for eg user has reddit and hackernews then call:
        // api/reddit/posts, api/hackernews/posts
        // or maybe make two separate endpoints, one for links, one for posts

        [HttpGet]
        [Route("posts")]
        public IActionResult GetPostsFromSubscribedSites()
        {
            string userId = User.GetUserId();

            var posts = _sitesService.GetPostsFromUserSites(userId);

            if (posts == null)
            {
                return NotFound();
            }


            // todo not sure if it should be like that
            // maybe just return nulls in response
            string jsonWithoutNulls = JsonConvert.SerializeObject(posts, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return Content(jsonWithoutNulls, "application/json");
        }

        [HttpPost]
        [Route("subscribe")]
        public IActionResult SubscribeToASite(UserSitesSubscribeRequest userSitesSubscribeRequest)
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
            var availableSites = AvailableSites.GetAll();

            return Ok(availableSites);
        }
    }


}

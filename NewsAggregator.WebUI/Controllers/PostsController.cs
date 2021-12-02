using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Services;
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
        [Route("getall")]
        public IActionResult GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var posts = _rssSiteService.GetAllPosts(paginationFilter);
                return Ok(posts);
            }
            catch (NotImplementedException ex)
            {
                return BadRequest("Wrong sitename");
            }
        }

        [HttpGet]
        [Route("getallbydaterange")]
        public IActionResult GetAllByDateRange([FromQuery] PaginationFilter paginationFilter, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var posts = _rssSiteService.GetAllPostsByDateRange(paginationFilter, startDate, endDate);
            return Ok(posts);
        }

    }
}

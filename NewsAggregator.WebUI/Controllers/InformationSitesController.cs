using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InformationSitesController : ControllerBase
    {
        private readonly ISiteFactory _siteFactory;
        private readonly IRssSitesService _rssSiteService;

        public InformationSitesController(IRssSitesService rssSiteService)
        {
            _rssSiteService = rssSiteService;
        }

        [HttpGet]
        [Route("getposts")]
        public IActionResult GetPosts([FromQuery] string sitename, [FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var posts = _rssSiteService.GetPosts(paginationFilter, sitename);
                return Ok(posts);
            }
            catch (NotImplementedException ex)
            {
                return BadRequest("Wrong sitename");
            }
        }

        [HttpGet]
        [Route("getpostsbydaterange")]
        public IActionResult GetPostsByDateRange([FromQuery] string sitename, [FromQuery] PaginationFilter paginationFilter, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var posts = _rssSiteService.GetPostsByDateRange(paginationFilter, sitename, startDate, endDate);
                return Ok(posts);
            }
            catch (NotImplementedException ex)
            {
                return BadRequest("Wrong sitename");
            }
        }

    }
}

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

    }
}

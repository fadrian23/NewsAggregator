using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Extensions;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Services;
using System;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InformationSitesController : ControllerBase
    {
        private readonly IRssSitesService _rssSiteService;

        public InformationSitesController(IRssSitesService rssSiteService)
        {
            _rssSiteService = rssSiteService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getpostsbydaterange")]
        public IActionResult GetPostsByDateRange(
            [FromQuery] string sitename,
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate
        )
        {
            try
            {
                var posts = _rssSiteService.GetPostsByDateRange(
                    paginationFilter,
                    startDate,
                    endDate,
                    null,
                    sitename
                );
                return Ok(posts);
            }
            catch (NotImplementedException ex)
            {
                return BadRequest("Wrong sitename");
            }
        }

        [HttpGet]
        [Route("getpostsbydaterangeauth")]
        public IActionResult GetPostsByDateRangeAuth(
            [FromQuery] string sitename,
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate
        )
        {
            var userId = User.GetUserId();
            try
            {
                var posts = _rssSiteService.GetPostsByDateRange(
                    paginationFilter,
                    startDate,
                    endDate,
                    userId,
                    sitename
                );
                return Ok(posts);
            }
            catch (NotImplementedException ex)
            {
                return BadRequest("Wrong sitename");
            }
        }
    }
}

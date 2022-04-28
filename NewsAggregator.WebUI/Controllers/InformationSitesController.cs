using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.Extensions;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models;
using System;
using System.Collections.Generic;

namespace NewsAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            _articlesService = articlesService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetArticles(
            int? feedId,
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] DateRange dateRange
        )
        {
            var articles = _articlesService.GetArticlesByDateRange(
                paginationFilter,
                dateRange.Start,
                dateRange.End,
                feedId
            );

            return articles.Success ? Ok(articles) : BadRequest();
        }
    }
}

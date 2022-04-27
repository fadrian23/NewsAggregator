using NewsAggregator.Data.Models;
using NewsAggregator.Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<RssArticle> Paginate(
            this IQueryable<RssArticle> input,
            PaginationFilter paginationFilter
        )
        {
            return input
                .OrderByDescending(x => x.DateTime)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
        }
    }
}

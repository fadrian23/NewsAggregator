using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Services
{
    public interface IArticlesService
    {
        PagedResponse<IEnumerable<RssArticleDTO>> GetPostsByDateRange(
            PaginationFilter paginationFilter,
            DateTime startDate,
            DateTime endDate,
            string userId = null,
            string sitename = null
        );
        PagedResponse<IEnumerable<RssArticleDTO>> GetPostsByDateRangeForLater(
            PaginationFilter paginationFilter,
            string userId
        );
        bool SavePostForLater(string userId, int postId);
        bool RemovePostForLater(string userId, int postId);
    }
}

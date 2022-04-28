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
        PagedResponse<IEnumerable<RssArticleDTO>> GetArticlesByDateRange(
            PaginationFilter paginationFilter,
            DateTime startDate,
            DateTime endDate,
            int? feedId = null
        );
        PagedResponse<IEnumerable<RssArticleDTO>> GetPostsByDateRangeForLater(
            PaginationFilter paginationFilter,
            string userId
        );
        bool SaveArticle(string userId, int postId);
        bool RemoveArticle(string userId, int postId);
    }
}

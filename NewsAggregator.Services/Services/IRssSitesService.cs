using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Services
{
    public interface IRssSitesService
    {
        PagedResponse<IEnumerable<RssPostDTO>> GetPostsByDateRange(
            PaginationFilter paginationFilter,
            DateTime startDate,
            DateTime endDate,
            string userId = null,
            string sitename = null
        );
        PagedResponse<IEnumerable<RssPostDTO>> GetPostsByDateRangeForLater(
            PaginationFilter paginationFilter,
            string userId
        );
        Task<KeyValuePair<string, IEnumerable<RssPost>>> GetArticlesFromRssFeed(
            string siteName,
            string URL
        );
        void SaveArticles(IEnumerable<RssPost> articles, string siteName);
        bool SavePostForLater(string userId, int postId);
        bool RemovePostForLater(string userId, int postId);
    }
}

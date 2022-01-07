using NewsAggregator.Data.Models;
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
    public interface IRssSitesService
    {
        PagedResponse<IEnumerable<RssPostDTO>> GetPostsByDateRange(PaginationFilter paginationFilter, DateTime startDate, DateTime endDate, string userId = null, string sitename = null);
        PagedResponse<IEnumerable<RssPostDTO>> GetPostsByDateRangeForLater(PaginationFilter paginationFilter, string userId);
        void FetchDataFromRssFeed(string siteName, string URL);
        bool SavePostForLater(string userId, int postId);
        bool RemovePostForLater(string userId, int postId);
    }
}

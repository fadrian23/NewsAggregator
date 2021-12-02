using NewsAggregator.Data.Models;
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
        PagedResponse<IEnumerable<RssPost>> GetAllPosts(PaginationFilter paginationFilter);
        PagedResponse<IEnumerable<RssPost>> GetAllPostsByDateRange(PaginationFilter paginationFilter, DateTime startDate, DateTime endDate);
        PagedResponse<IEnumerable<RssPost>> GetPosts(PaginationFilter paginationFilter, string sitename);
        PagedResponse<IEnumerable<RssPost>> GetPostsByDateRange(PaginationFilter paginationFilter, string sitename, DateTime startDate, DateTime endDate);
        void FetchDataFromRssFeed(string siteName, string URL);
    }
}

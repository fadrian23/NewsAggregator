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
        PagedResponse<IEnumerable<RssPost>> GetPosts(PaginationFilter paginationFilter, string sitename);
        void FetchDataFromRssFeed(string siteName, string URL);
    }
}

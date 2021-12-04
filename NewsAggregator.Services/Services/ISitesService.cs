using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using System.Collections.Generic;

namespace NewsAggregator.Services.Services
{
    public interface ISitesService
    {
        PagedResponse<UserSitesPostsDTO> GetPostsFromUserSites(string userId, PaginationFilter paginationFilter);
        SiteSubscriptionResult SubscribeToSites(IEnumerable<string> sites, string userId);
        List<string> GetSubscribedSites(string userId);
    }
}
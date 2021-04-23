using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.HelperModels;
using System.Collections.Generic;

namespace NewsAggregator.Services.Services
{
    public interface ISitesService
    {
        UserSitesPostsDTO GetPostsFromUserSites(string userId);
        SiteSubscriptionResult SubscribeToSites(IEnumerable<string> sites, string userId);
    }
}
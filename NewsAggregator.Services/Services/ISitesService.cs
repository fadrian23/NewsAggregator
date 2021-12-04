using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using System;
using System.Collections.Generic;

namespace NewsAggregator.Services.Services
{
    public interface ISitesService
    {
        PagedResponse<IEnumerable<RssPostDTO>> GetPostsFromUserSites(string userId, PaginationFilter paginationFilter, DateTime startDate, DateTime endDate);
        SiteSubscriptionResult SubscribeToSites(IEnumerable<string> sites, string userId);
        List<string> GetSubscribedSites(string userId);
    }
}
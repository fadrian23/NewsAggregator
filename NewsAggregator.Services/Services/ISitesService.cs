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
        PagedResponse<IEnumerable<RssArticleDTO>> GetArticlesFromSubscribedFeeds(
            string userId,
            PaginationFilter paginationFilter,
            DateTime startDate,
            DateTime endDate
        );
        SiteSubscriptionResult SubscribeToFeeds(IEnumerable<int> feedIds, string userId);
        IEnumerable<RssFeedDTO> GetSubscribedFeeds(string userId);
        IEnumerable<RssFeedDTO> GetAvailableFeeds();
        RssFeedDTO GetFeedById(int id);
    }
}

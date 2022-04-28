using Microsoft.EntityFrameworkCore;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Extensions;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Services.Implementation
{
    public class SitesService : ISitesService
    {
        private readonly ApplicationDbContext _context;

        public SitesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedResponse<IEnumerable<RssArticleDTO>> GetArticlesFromSubscribedFeeds(
            string userId,
            PaginationFilter paginationFilter,
            DateTime startDate,
            DateTime endDate
        )
        {
            var articlesFromSubscribedFeeds = _context.RssArticles
                .Where(
                    article =>
                        _context.ApplicationUserSettings
                            .FirstOrDefault(x => x.UserId == userId)
                            .RssFeeds.Select(x => x.RssFeedId)
                            .Contains(article.RssFeedId)
                )
                .Where(article => article.DateTime <= endDate && article.DateTime >= startDate);

            var articlesCount = articlesFromSubscribedFeeds.Count();

            var paginatedArticleDTOs = articlesFromSubscribedFeeds
                .Paginate(paginationFilter)
                .Select(
                    article =>
                        new RssArticleDTO
                        {
                            Id = article.Id,
                            DateTime = article.DateTime,
                            Description = article.Description,
                            SiteName = article.RssFeed.Name,
                            Title = article.Title,
                            URL = article.URL,
                            IsSavedForLater = _context.ApplicationUserSettings
                                .FirstOrDefault(x => x.UserId == userId)
                                .SavedArticles.Any(x => x.Id == article.Id)
                        }
                )
                .ToList();

            return new PagedResponse<IEnumerable<RssArticleDTO>>(
                paginatedArticleDTOs,
                paginationFilter.PageNumber,
                paginationFilter.PageSize,
                articlesCount
            );
        }

        public IEnumerable<RssFeedDTO> GetSubscribedFeeds(string userId)
        {
            var rssFeeds = _context.ApplicationUserSettings
                .Include(x => x.RssFeeds)
                .FirstOrDefault(x => x.UserId == userId)
                .RssFeeds.Select(
                    x =>
                        new RssFeedDTO
                        {
                            Id = x.RssFeedId,
                            Name = x.Name,
                            URL = x.URL,
                            ParentFeedId = x.ParentFeedId
                        }
                )
                .ToList();

            return rssFeeds;
        }

        public IEnumerable<RssFeedDTO> GetAvailableFeeds()
        {
            var feeds = _context.RssFeeds
                .Select(
                    x =>
                        new RssFeedDTO
                        {
                            Id = x.RssFeedId,
                            Name = x.Name,
                            ParentFeedId = x.ParentFeedId,
                            URL = x.URL,
                        }
                )
                .ToList();

            return feeds;
        }

        public SiteSubscriptionResult SubscribeToFeeds(IEnumerable<int> feedIds, string userId)
        {
            var rssFeeds = _context.RssFeeds
                .Where(feed => feedIds.Contains(feed.RssFeedId))
                .ToList();

            var unknownFeedIds = feedIds.Where(
                feed => !rssFeeds.Select(x => x.RssFeedId).Contains(feed)
            );

            if (unknownFeedIds.Any())
            {
                return new SiteSubscriptionResult
                {
                    Errors = unknownFeedIds
                        .Select(feedId => $"Could not find feed with id {feedId}")
                        .ToList(),
                    Success = false
                };
            }

            _context.ApplicationUserSettings
                .Include(x => x.RssFeeds)
                .FirstOrDefault(x => x.UserId == userId)
                .RssFeeds.AddRange(rssFeeds);

            _context.SaveChanges();

            return new SiteSubscriptionResult { Errors = null, Success = true };
        }
    }
}

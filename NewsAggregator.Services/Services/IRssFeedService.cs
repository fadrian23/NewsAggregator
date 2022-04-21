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
    public interface IRssFeedService
    {
        Task<KeyValuePair<string, IEnumerable<RssArticle>>> GetArticlesFromRssFeed(
            string siteName,
            string URL
        );
        void SaveArticles(IEnumerable<RssArticle> articles, string siteName);
    }
}

using Hangfire;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class ScrapeJob : IScrapeJob
    {
        private readonly IRssFeedService _rssSitesService;
        private readonly ApplicationDbContext _context;

        public ScrapeJob(IRssFeedService rssSitesService, ApplicationDbContext context)
        {
            _rssSitesService = rssSitesService;
            _context = context;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task ScrapeRSSFeeds()
        {
            var availableFeeds = _context.RssFeeds.ToList();

            var tasks = availableFeeds.Select(
                feed => _rssSitesService.GetArticlesFromRssFeed(feed)
            );

            // Array element is a KeyValuePair where Key is site name and Value is list of articles from this site
            var allArticles = await Task.WhenAll(tasks);

            foreach (var articles in allArticles)
            {
                _rssSitesService.SaveArticles(articles.Value, articles.Key);
            }
        }
    }
}

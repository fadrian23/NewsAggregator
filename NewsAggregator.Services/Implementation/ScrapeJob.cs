using Hangfire;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class ScrapeJob : IScrapeJob
    {
        private readonly IRssSitesService _rssSitesService;

        public ScrapeJob(IRssSitesService rssSitesService)
        {
            _rssSitesService = rssSitesService;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task ScrapeRSSFeeds()
        {
            var tasks = AvailableRssFeeds.RssFeeds.Select(
                feed => _rssSitesService.GetArticlesFromRssFeed(feed.Key, feed.Value)
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

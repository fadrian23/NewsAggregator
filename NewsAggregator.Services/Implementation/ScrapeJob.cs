using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;

namespace NewsAggregator.Services.Implementation
{
    public class ScrapeJob : IScrapeJob
    {
        private readonly ISiteFactory _siteFactory;
        private readonly ILogger<ScrapeJob> _logger;
        private readonly IRssSitesService _rssSitesService;

        public ScrapeJob(ISiteFactory siteFactory, ILogger<ScrapeJob> logger, IRssSitesService rssSitesService)
        {
            _siteFactory = siteFactory;
            _logger = logger;
            _rssSitesService = rssSitesService;
        }

        [AutomaticRetry(Attempts = 0)]
        public void ScrapSites()
        {
            _logger.LogInformation("Getting data from external APIs");
            foreach (var site in AvailableSites.GetAll())
            {
                _siteFactory.For(site).GetAndSaveData();
            }
        }

        [AutomaticRetry(Attempts = 0)]
        public void GetDataFromRssFeeds()
        {
            _logger.LogInformation("Getting data from rss feeds.");
            foreach (var site in AvailableRssFeeds.RssFeeds)
            {
                _rssSitesService.FetchDataFromRssFeed(site.Key, site.Value);
            }
        }




    }
}

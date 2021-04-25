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

        public ScrapeJob(ISiteFactory siteFactory, ILogger<ScrapeJob> logger)
        {
            _siteFactory = siteFactory;
            _logger = logger;
        }

        [AutomaticRetry(Attempts = 0)]
        public void ScrapSites()
        {
            _logger.LogInformation("Running a scrapjob");
            foreach (var site in AvailableSites.GetAll())
            {
                _siteFactory.For(site).GetAndSaveData();
            }

        }




    }
}

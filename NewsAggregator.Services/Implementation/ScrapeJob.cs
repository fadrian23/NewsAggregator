using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using NewsAggregator.Services.Services;

namespace NewsAggregator.Services.Implementation
{
    public class ScrapeJob : IScrapeJob
    {
        private readonly IEnumerable<ISiteService> _socialServices;
        private readonly ILogger<ScrapeJob> _logger;

        public ScrapeJob(IEnumerable<ISiteService> socialServices, ILogger<ScrapeJob> logger)
        {
            _socialServices = socialServices;
            _logger = logger;
        }

        [AutomaticRetry(Attempts = 0)]
        public void ScrapSites()
        {
            _logger.LogInformation("Running a scrapjob");
            foreach (var service in _socialServices)
            {
                service.GetAndSaveData();
            }
        }




    }
}

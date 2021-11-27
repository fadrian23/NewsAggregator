using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class SiteFactory : ISiteFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SiteFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISiteService For(string userSelection)
        {
            return userSelection switch
            {
                AvailableSites.Reddit => (ISiteService)_serviceProvider.GetService(typeof(RedditService)),
                AvailableSites.HackerNews => (ISiteService)_serviceProvider.GetService(typeof(HackerNewsService)),
                _ => throw new NotImplementedException($"not implemented service for {userSelection}")
            };
        }

        public IRssSitesService ForRssSite(string userSelection)
        {
            var x = (IRssSitesService)_serviceProvider.GetService(typeof(IRssSitesService));

            return x;
        }

    }
}

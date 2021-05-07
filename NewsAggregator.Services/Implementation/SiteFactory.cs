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
                //AvailableSites.PolsatNews => (ISiteService)_serviceProvider.GetService(typeof(PolsatNewsService)),
                _ => throw new NotImplementedException($"not implemented service for {userSelection}")
            };
        }

    }
}

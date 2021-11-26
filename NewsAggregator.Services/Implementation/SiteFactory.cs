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
            var x = _serviceProvider.GetService(typeof(RssSitesService));
            Console.WriteLine("from sitefactory");
            System.Console.WriteLine(x);

            return userSelection switch
            {
                AvailableSites.PolsatNews => (IRssSitesService)_serviceProvider.GetService(typeof(IRssSitesService)),
                _ => throw new NotImplementedException($"not implemented service for {userSelection}"),
            };
        }

    }
}

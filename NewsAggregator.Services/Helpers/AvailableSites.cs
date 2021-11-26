using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Helpers
{
    public static class AvailableSites
    {
        public const string Reddit = "Reddit";
        public const string HackerNews = "HackerNews";
        public const string PolsatNews = "PolsatNews";


        public static IEnumerable<string> GetAll() => new List<string> { Reddit, HackerNews, PolsatNews };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Helpers
{
    public static class AvailableRssFeeds
    {
        public static (string siteName, string feedURL) PolsatNews => ("PolsatNews", "https://www.polsatnews.pl/rss/wszystkie.xml");



        public static IEnumerable<(string siteName, string feedURL)> GetAll() => new List<(string, string)> { PolsatNews };
    }
}

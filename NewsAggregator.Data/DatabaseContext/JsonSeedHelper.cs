using NewsAggregator.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Data.DatabaseContext
{
    public static class JsonSeedHelper
    {
        public static IEnumerable<RssFeed> GetRssFeedData()
        {
            var feedsJson = File.ReadAllText(@"../NewsAggregator.Data/SeedData/RssFeeds.json");

            return JsonConvert.DeserializeObject<List<RssFeed>>(feedsJson);
        }
    }
}

using System.Collections.Generic;

namespace NewsAggregator.Services.Helpers
{
    public static class AvailableSites
    {
        public const string Reddit = "Reddit";
        public const string HackerNews = "HackerNews";

        public static IEnumerable<string> GetAll() => new List<string> { Reddit, HackerNews };
    }
}

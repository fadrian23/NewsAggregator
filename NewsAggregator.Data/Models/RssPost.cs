using System;
using System.Collections.Generic;

namespace NewsAggregator.Data.Models
{
    public class RssArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }

        public int RssFeedId { get; set; }
        public RssFeed RssFeed { get; set; }
        public IEnumerable<ApplicationUserSettings> ApplicationUserSettings { get; set; }
    }
}

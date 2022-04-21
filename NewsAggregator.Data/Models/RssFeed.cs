using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Data.Models
{
    public class RssFeed
    {
        public int RssFeedId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public int? ParentFeedId { get; set; }
        public RssFeed ParentFeed { get; set; }

        public List<RssFeed> SubFeeds { get; set; }
        public List<RssArticle> Articles { get; set; }
        public List<ApplicationUserSettings> UserSettings { get; set; }
    }
}

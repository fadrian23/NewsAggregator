using NewsAggregator.Data.Models.Identity;
using System.Collections.Generic;

namespace NewsAggregator.Data.Models
{
    public class ApplicationUserSettings
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<RssFeed> RssFeeds { get; set; }

        public List<RssArticle> SavedArticles { get; set; }
    }
}

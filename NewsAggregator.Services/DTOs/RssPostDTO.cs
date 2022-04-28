using NewsAggregator.Data.Models;
using System;

namespace NewsAggregator.Services.DTOs
{
    public class RssArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }
        public int RssFeedId { get; set; }
        public bool IsSavedForLater { get; set; }
    }
}

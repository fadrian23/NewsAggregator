using System;

namespace NewsAggregator.Services.DTOs
{
    public class RssPostDTO : ISocialModelDTO
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }

        public bool IsSavedForLater { get; set; }
    }
}

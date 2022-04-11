using System;
using System.Collections.Generic;

namespace NewsAggregator.Data.Models
{
    public class RssPost
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<ApplicationUserSettings> ApplicationUserSettings { get; set; }
    }
}

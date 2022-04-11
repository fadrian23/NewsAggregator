using System.Collections.Generic;

namespace NewsAggregator.Data.Models
{
    public class SiteName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ApplicationUserSettings> ApplicationUserSettings { get; set; }
    }
}

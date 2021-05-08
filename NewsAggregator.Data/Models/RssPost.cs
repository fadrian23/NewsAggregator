using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Data.Models
{
    public class RssPost : ISocialModel
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }
        public PostCategory PostCategory { get; set; }
    }
}

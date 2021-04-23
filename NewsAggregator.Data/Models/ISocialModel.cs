using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Data.Models
{
    public interface ISocialModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }
        public PostCategory PostCategory { get; set; }
    }
}

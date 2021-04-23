using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.DTOs
{
    public class HackerNewsPostDTO : ISocialModelDTO
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Data.Models
{
    public class HackerNewsPost : IPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }
        public PostCategory PostCategory { get; set; }
    }

    public class HackerNewsModel
    {
        public string By { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public PostCategory PostCategory { get; set; }
    }
}

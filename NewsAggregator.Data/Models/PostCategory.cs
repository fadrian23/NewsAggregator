using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Data.Models
{
    public class PostCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Keyword> Keywords { get; set; }
    }

    public class Keyword
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostCategoryId { get; set; }
        public PostCategory PostCategory { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace NewsAggregator.Data.Models
{
    public class RedditPost : ISocialModel
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public string Subreddit { get; set; }
        public string Author { get; set; }
        public int Score { get; set; }
        public PostCategory PostCategory { get; set; }
    }

    public class RedditRoot
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public List<Child> Children { get; set; }
    }

    public class Child
    {
        public Data2 Data { get; set; }
    }

    public class Data2
    {
        public string Title { get; set; }
        public string Subreddit { get; set; }
        public string Author { get; set; }
        public int Score { get; set; }
        public string Permalink { get; set; }
    }

}


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.DTOs
{
    public class CategoryPostsDTO
    {
        public IEnumerable<RedditPostDTO> RedditPosts { get; set; }
        public IEnumerable<HackerNewsPostDTO> HackerNewsPosts { get; set; }
    }
}
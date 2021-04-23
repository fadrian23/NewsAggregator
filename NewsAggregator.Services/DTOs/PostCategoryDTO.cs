﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.DTOs
{
    public class PostCategoryDTO
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public List<string> Keywords { get; set; }
    }

    public class KeywordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
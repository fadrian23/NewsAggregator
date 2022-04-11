﻿using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using System;
using System.Collections.Generic;

namespace NewsAggregator.Services.Services
{
    public interface ISiteService
    {
        public PagedResponse<IEnumerable<ISocialModelDTO>> GetPosts(
            PaginationFilter paginationFilter
        );
        public IEnumerable<ISocialModelDTO> GetPostsByDate(DateTime Date);
        public bool GetAndSaveData();
    }
}

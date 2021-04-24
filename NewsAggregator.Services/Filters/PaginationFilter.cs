using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Filters
{
    public class PaginationFilter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public PaginationFilter()
        {
            PageSize = 5;
            PageNumber = 1;
        }

        public PaginationFilter(int pageSize, int pageNumber)
        {
            PageSize = pageSize > 20 ? 20 : pageSize;
            PageNumber = pageNumber > 0 ? pageNumber : 1;
        }
    }
}

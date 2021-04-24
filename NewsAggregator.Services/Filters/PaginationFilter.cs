using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Filters
{
    public class PaginationFilter
    {
        private int _PageSize = 10;
        public int PageSize
        {
            get => _PageSize;
            set
            {
                _PageSize = (value > 20) ? 20 : value;
            }
        }
        public int PageNumber { get; set; }

        public PaginationFilter()
        {
            PageSize = 5;
            PageNumber = 1;
        }
    }
}

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
        private int _PageNumber = 1;
        public int PageNumber
        {
            get => _PageNumber;
            set
            {
                _PageNumber = (value >= 1) ? value : 1;
            }
        }

        public PaginationFilter()
        {
            PageSize = 5;
            PageNumber = 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.HelperModels
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool Success { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int count)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = count / pageSize;
            TotalRecords = count;

            Data = data;
            Success = true;
            Errors = null;
        }


    }
}

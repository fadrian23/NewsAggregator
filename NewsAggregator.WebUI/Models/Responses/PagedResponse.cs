using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool Success { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Errors = null;
            Success = true;
        }
    }
}

using System.Collections.Generic;

namespace NewsAggregator.Services.HelperModels
{
    public class Response<T>
    {
        public Response(T data)
        {
            Data = data;
            Success = true;
            Errors = null;
        }

        public T Data { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

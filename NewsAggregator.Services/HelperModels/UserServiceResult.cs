using System.Collections.Generic;

namespace NewsAggregator.Services.HelperModels
{
    public class UserServiceResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

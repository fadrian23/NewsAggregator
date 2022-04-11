using System.Collections.Generic;

namespace NewsAggregator.Services.HelperModels
{
    public class SiteSubscriptionResult
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
    }
}

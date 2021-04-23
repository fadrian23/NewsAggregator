using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.HelperModels
{
    public class SiteSubscriptionResult
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
    }
}

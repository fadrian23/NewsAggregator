using System.Collections.Generic;

namespace NewsAggregator.WebUI.Models.Requests
{
    public class UserSitesSubscribeRequest
    {
        public IEnumerable<string> Sites { get; set; }
    }
}

using System.Collections.Generic;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class UserSitesSubscribeResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

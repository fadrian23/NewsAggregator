using NewsAggregator.Services.HelperModels;
using System.Collections.Generic;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class FailedAuthResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public AuthenticationResultType Result { get; set; }
    }
}

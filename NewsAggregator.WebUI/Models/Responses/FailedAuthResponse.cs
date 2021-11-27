using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAggregator.Services.HelperModels;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class FailedAuthResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public AuthenticationResultType Result { get; set; }
    }
}

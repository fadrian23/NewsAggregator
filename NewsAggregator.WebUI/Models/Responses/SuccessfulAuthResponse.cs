using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class SuccessfulAuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

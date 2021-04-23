using NewsAggregator.Services.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class RestService : IRestService
    {
        public RestClient CreateClient(string URL)
        {
            return new RestClient(URL);
        }
    }
}

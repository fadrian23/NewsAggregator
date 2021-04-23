using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
namespace NewsAggregator.Services.Services
{
    public interface IRestService
    {
        RestClient CreateClient(string URL);
    }
}

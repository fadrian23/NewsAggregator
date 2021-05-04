using NewsAggregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Services
{
    public interface IRssFeedService
    {
        IEnumerable<RssPost> GetData(string siteName, string URL);
    }
}

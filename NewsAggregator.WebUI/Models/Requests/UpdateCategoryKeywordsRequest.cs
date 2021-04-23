using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Models.Requests
{
    public class UpdateCategoryKeywordsRequest
    {
        public List<string> Keywords { get; set; }
    }
}

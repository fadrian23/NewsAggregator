using NewsAggregator.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class CategoryKeywordsResponse
    {
        public List<KeywordDTO> Keywords { get; set; }
    }
}

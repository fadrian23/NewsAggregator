using NewsAggregator.Services.DTOs;
using System.Collections.Generic;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class CategoryKeywordsResponse
    {
        public List<KeywordDTO> Keywords { get; set; }
    }
}

using System.Collections.Generic;

namespace NewsAggregator.WebUI.Models.Requests
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; }
        public List<string> Keywords { get; set; }
    }
}

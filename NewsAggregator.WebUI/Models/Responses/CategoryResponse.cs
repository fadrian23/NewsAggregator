using System.Collections.Generic;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Keywords { get; set; }
    }
}

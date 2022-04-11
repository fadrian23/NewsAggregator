using NewsAggregator.Services.DTOs;
using System.Collections.Generic;

namespace NewsAggregator.WebUI.Models.Responses
{
    public class UserSitesPostsResponse
    {
        public IEnumerable<RedditPostDTO> RedditPosts { get; set; }
        public IEnumerable<HackerNewsPostDTO> HackerNewsPosts { get; set; }
    }
}

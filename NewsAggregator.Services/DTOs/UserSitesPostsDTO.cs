using System.Collections.Generic;

namespace NewsAggregator.Services.DTOs
{
    public class UserSitesPostsDTO
    {
        public IEnumerable<RedditPostDTO> RedditPosts { get; set; }
        public IEnumerable<HackerNewsPostDTO> HackerNewsPosts { get; set; }

        public IEnumerable<RssPostDTO> RssPosts { get; set; }
    }
}

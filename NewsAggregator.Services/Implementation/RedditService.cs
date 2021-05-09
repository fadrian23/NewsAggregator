using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using NewsAggregator.Services.Services;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.HelperModels;

namespace NewsAggregator.Services.Implementation
{
    public class RedditService : ISiteService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<RedditService> _logger;
        private readonly IPostCategorizationService _postCategorizationService;

        public RedditService(ApplicationDbContext context, ILogger<RedditService> logger, IPostCategorizationService postCategorizationService)
        {
            _context = context;
            _logger = logger;
            _postCategorizationService = postCategorizationService;
        }

        public PagedResponse<IEnumerable<ISocialModelDTO>> GetPosts(PaginationFilter paginationFilter)
        {
            IEnumerable<ISocialModelDTO> posts = _context
                .RedditPosts
                .OrderByDescending(x => x.DateTime)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .Select(x => new RedditPostDTO
                {
                    Id = x.Id,
                    URL = x.URL,
                    Title = x.Title,
                    Score = x.Score,
                    Subreddit = x.Subreddit,
                    Author = x.Author
                });
            var postsCount = _context.RedditPosts.Count();

            return new PagedResponse<IEnumerable<ISocialModelDTO>>
                (
                    posts,
                    paginationFilter.PageNumber,
                    paginationFilter.PageSize,
                    postsCount
                );
        }

        private List<RedditPost> FetchPosts(IEnumerable<string> Subreddits, int numberOfPosts = 20)
        {
            List<RedditPost> RedditPosts = new();

            foreach (var subreddit in Subreddits)
            {
                //var restClient = _restService.CreateClient($"https://www.reddit.com/r/{subreddit}/");
                var restClient = new RestClient($"https://www.reddit.com/r/{subreddit}/");
                var request = new RestRequest($"top.json?limit={numberOfPosts}", DataFormat.Json);
                var response = restClient.Execute<RedditRoot>(request);
                var RedditPost = JsonConvert.DeserializeObject<RedditRoot>(response.Content);
                foreach (var item in RedditPost.Data.Children)
                {
                    var redditPost = new RedditPost
                    {
                        Author = item.Data.Author,
                        Score = item.Data.Score,
                        Subreddit = item.Data.Subreddit,
                        Title = item.Data.Title,
                        DateTime = DateTime.Now,
                        URL = $"http://www.reddit.com{item.Data.Permalink}",
                        PostCategory = _context.PostCategories.SingleOrDefault(x => x.Name.ToLower() == subreddit.ToLower())
                    };
                    RedditPosts.Add(redditPost);
                }
            }

            return RedditPosts;
        }



        public bool GetAndSaveData()
        {
            var availableSubreddits = AvailableRedditSubreddits.GetAll();
            var data = FetchPosts(availableSubreddits);

            _logger.LogInformation("fetching data from reddit");

            foreach (var item in data)
            {
                if (!_context.RedditPosts.Any(x => x.Title == item.Title && x.Author == item.Author && x.Subreddit == item.Subreddit))
                {
                    var post = item;
                    post = (RedditPost)_postCategorizationService.CategorizePost(post);
                    _context.RedditPosts.Add(post);
                }
            }

            if (_context.SaveChanges() > 0)
            {
                _logger.LogInformation("fetching data from reddit done");
                return true;
            }
            _logger.LogInformation("no new posts in reddit");
            return false;
        }

        public IEnumerable<ISocialModelDTO> GetPostsByDate(DateTime Date)
        {
            IEnumerable<ISocialModelDTO> posts = _context.RedditPosts.Where(x => x.DateTime == Date).Select(z => new RedditPostDTO
            {
                Id = z.Id,
                Subreddit = z.Subreddit,
                Score = z.Score,
                Author = z.Author,
                Title = z.Title,
                URL = z.URL
            });

            return posts;
        }

    }
}

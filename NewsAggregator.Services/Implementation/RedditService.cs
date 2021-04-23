﻿using System;
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

namespace NewsAggregator.Services.Implementation
{
    public class RedditService : IRedditService
    {

        private readonly IRestService _restService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RedditService> _logger;
        private readonly ICategoryService _categoryService;

        public RedditService(IRestService restService, ApplicationDbContext context, ILogger<RedditService> logger, ICategoryService categoryService)
        {
            _restService = restService;
            _context = context;
            _logger = logger;
            _categoryService = categoryService;
        }



        public IEnumerable<ISocialModelDTO> GetPosts()
        {
            IEnumerable<ISocialModelDTO> posts = _context.RedditPosts.Select(x => new RedditPostDTO
            {
                Id = x.Id,
                URL = x.URL,
                Title = x.Title,
                Score = x.Score,
                Subreddit = x.Subreddit,
                Author = x.Author
            }).ToList();

            return posts;


        }

        private List<RedditPost> FetchPosts()
        {
            var restClient = _restService.CreateClient("https://www.reddit.com/r/all/");
            var request = new RestRequest("top.json?limit=20", DataFormat.Json);
            var response = restClient.Execute<RedditRoot>(request);

            var RedditPost = JsonConvert.DeserializeObject<RedditRoot>(response.Content);//

            var RedditPosts = new List<RedditPost>();

            foreach (var item in RedditPost.Data.Children)
            {
                var redditPost = new RedditPost
                {
                    Author = item.Data.Author,
                    Score = item.Data.Score,
                    Subreddit = item.Data.Subreddit,
                    Title = item.Data.Title,
                    DateTime = DateTime.Now.Date,
                    URL = $"http://www.reddit.com{item.Data.Permalink}"
                };
                RedditPosts.Add(redditPost);
            }

            return RedditPosts;
        }


        public bool GetAndSaveData()
        {
            var data = FetchPosts();

            _logger.LogInformation("fetching data from reddit");

            foreach (var item in data)
            {
                if (!_context.RedditPosts.Any(x => x.Title == item.Title && x.Author == item.Author && x.Subreddit == item.Subreddit))
                {
                    var post = item;
                    post = (RedditPost)_categoryService.CategorizePost(post);
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

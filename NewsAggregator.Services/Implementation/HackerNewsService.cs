using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class HackerNewsService : ISiteService
    {
        private readonly IRestService _restService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HackerNewsService> _logger;

        public HackerNewsService(IRestService restService, ApplicationDbContext context, ILogger<HackerNewsService> logger)
        {
            _restService = restService;
            _context = context;
            _logger = logger;
        }


        public PagedResponse<IEnumerable<ISocialModelDTO>> GetPosts(PaginationFilter paginationFilter)
        {
            IEnumerable<HackerNewsPostDTO> posts = _context.HackerNewsPosts
                .OrderByDescending(x => x.DateTime)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .Select(x => new HackerNewsPostDTO
                {
                    Author = x.Author,
                    Id = x.Id,
                    Title = x.Title,
                    URL = x.URL,
                });
            var postsCount = _context.HackerNewsPosts.Count();

            return new PagedResponse<IEnumerable<ISocialModelDTO>>
                (
                     posts,
                     paginationFilter.PageNumber,
                     paginationFilter.PageSize,
                     postsCount
                );

        }

        private List<int> GetTopItemsId()
        {
            var restClient = _restService.CreateClient("https://hacker-news.firebaseio.com/v0/");
            var request = new RestRequest("topstories.json?limitToFirst=20&orderBy=\"$key\"", DataFormat.Json);
            var response = restClient.Get(request);

            var TopItems = JsonConvert.DeserializeObject<List<int>>(response.Content);

            return TopItems;
        }

        private List<HackerNewsModel> FetchPosts()
        {
            var TopItemsIds = GetTopItemsId();

            List<HackerNewsModel> TopPosts = new();
            List<Task<HackerNewsModel>> tasks = new();

            foreach (var Id in TopItemsIds)
            {
                tasks.Add(Task.Run(() => GetPost(Id)));
            }

            TopPosts = Task.WhenAll(tasks).Result.ToList();
            return TopPosts;
        }

        private HackerNewsModel GetPost(int id)
        {
            var restClient = _restService.CreateClient("https://hacker-news.firebaseio.com/v0/item/");
            var request = new RestRequest($"{id}.json", DataFormat.Json);
            var response = restClient.Get(request);

            var hackerNewsModel = JsonConvert.DeserializeObject<HackerNewsModel>(response.Content);

            return hackerNewsModel;
        }


        public bool GetAndSaveData()
        {
            _logger.LogInformation("fetching data from hackernews");

            var PostsFromHackerNews = FetchPosts();


            foreach (var item in PostsFromHackerNews)
            {
                var post = new HackerNewsPost
                {
                    Author = item.By,
                    Title = item.Title,
                    URL = item.URL,
                    DateTime = DateTime.Now,
                };

                // if a post has same author and content then don't add same to db.
                if (!_context.HackerNewsPosts.Any(x => x.Title == post.Title && x.Author == post.Author))
                {
                    _context.HackerNewsPosts.Add(post);
                }
            }
            if (_context.SaveChanges() > 0)
            {
                _logger.LogInformation("fetching data from hackernews done");
                return true;
            }
            _logger.LogInformation("no new data from hackernews");
            return false;
        }



        public IEnumerable<ISocialModelDTO> GetPostsByDate(DateTime Date)
        {
            var posts = _context.HackerNewsPosts.Where(x => x.DateTime == Date).Select(z => new HackerNewsPostDTO
            {
                Id = z.Id,
                URL = z.URL,
                Title = z.Title,
                Author = z.Author,
            });

            return posts;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryService> _logger;
        private readonly ISiteFactory _siteFactory;

        public CategoryService(ApplicationDbContext context, ILogger<CategoryService> logger, ISiteFactory siteFactory)
        {
            _context = context;
            _logger = logger;
            _siteFactory = siteFactory;
        }

        public List<PostCategoryDTO> GetCategories(PaginationFilter paginationFilter)
        {
            var categories = _context
                .PostCategories
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .Select(x => new PostCategoryDTO
                {
                    Id = x.Id,
                    Category = x.Name,
                    Keywords = x.Keywords.Select(k => k.Name).ToList()
                }).ToList();

            if (categories == null)
            {
                return null;
            }

            return categories;
        }

        public PostCategoryDTO GetCategory(int id)
        {
            var postCategoryFromDB = _context.PostCategories.Include(k => k.Keywords).FirstOrDefault(pc => pc.Id == id);

            if (postCategoryFromDB == null)
            {
                return null;
            }

            var postCategory = new PostCategoryDTO
            {
                Id = postCategoryFromDB.Id,
                Category = postCategoryFromDB.Name,
                Keywords = postCategoryFromDB.Keywords.Select(x => x.Name).ToList()
            };

            return postCategory;
        }

        public void AddCategory(PostCategory postCategory)
        {
            _context.PostCategories.Add(postCategory);
            _context.SaveChanges();
        }

        public bool EditCategory(int id, PostCategory postCategory)
        {
            var categoryFromDb = _context.PostCategories.Include(k => k.Keywords).FirstOrDefault(x => x.Id == id);

            if (categoryFromDb == null)
            {
                return false;
            }

            _context.Entry(categoryFromDb).CurrentValues.SetValues(postCategory);
            categoryFromDb.Keywords = postCategory.Keywords;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteCategory(int id)
        {
            var postCategory = _context.PostCategories.Include(k => k.Keywords).FirstOrDefault(pc => pc.Id == id);

            if (postCategory == null)
            {
                return false;
            }

            _context.PostCategories.Remove(postCategory);
            return _context.SaveChanges() > 0;
        }

        public List<KeywordDTO> GetCategoryKeywords(int id)
        {
            var postCategory = _context.PostCategories.Include(k => k.Keywords).FirstOrDefault(pc => pc.Id == id);
            if (postCategory == null || postCategory.Keywords.Count == 0)
            {
                return null;
            }

            List<KeywordDTO> keywords = postCategory.Keywords.Select(x => new KeywordDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return keywords;

        }
        public bool AddKeywordsToCategory(List<Keyword> keywords, int postCategoryId)
        {
            var postCategoryFromDB = _context.PostCategories.Include(k => k.Keywords).FirstOrDefault(p => p.Id == postCategoryId);
            if (postCategoryFromDB == null)
            {
                return false;
            }

            foreach (var keyword in keywords)
            {
                postCategoryFromDB.Keywords.Add(new Keyword { Name = keyword.Name });
            }

            _context.SaveChanges();
            return true;
        }

        public bool DeleteCategoryKeywords(int categoryId)
        {
            var categoryKeywordsFromDB = _context.PostCategories.Include(k => k.Keywords).FirstOrDefault(pc => pc.Id == categoryId);
            if (categoryKeywordsFromDB == null)
            {
                return false;
            }
            categoryKeywordsFromDB.Keywords.RemoveAll(k => k.PostCategoryId == categoryId);

            return _context.SaveChanges() > 0;
        }


        public CategoryPostsDTO GetPostsOfCategory(int categoryId, PaginationFilter paginationFilter)
        {
            var hackernewsService = _siteFactory.For(AvailableSites.HackerNews);
            var redditService = _siteFactory.For(AvailableSites.Reddit);


            var response = new CategoryPostsDTO
            {
                RedditPosts = (IEnumerable<RedditPostDTO>)redditService.GetPosts(paginationFilter),
                HackerNewsPosts = (IEnumerable<HackerNewsPostDTO>)hackernewsService.GetPosts(paginationFilter),
                //todo not sure if split rss posts to for eg. "tvn24" "polsatnews" or keep them together
                //probably split them
                //PolsatNewsPosts = (IEnumerable<PolsatNewsPostDTO>)

            };

            return response;
        }

        public ISocialModel CategorizePost(ISocialModel post)
        {
            var postCategoriesList = _context.PostCategories.Include(k => k.Keywords).ToList();
            foreach (var postCategory in postCategoriesList)
            {
                if (postCategory.Keywords.Any(s => post.Title.Contains(s.Name)))
                {
                    _logger.LogInformation("post contains keyword!" + post.Title);
                    post.PostCategory = postCategory;
                }
            }

            return post;
        }

    }
}

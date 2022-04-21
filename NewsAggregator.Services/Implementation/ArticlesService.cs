using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArticlesService> _logger;

        public ArticlesService(ApplicationDbContext context, ILogger<ArticlesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public PagedResponse<IEnumerable<RssPostDTO>> GetPostsByDateRange(
            PaginationFilter paginationFilter,
            DateTime startDate,
            DateTime endDate,
            string userId = null,
            string sitename = null
        )
        {
            var posts = _context.RssArticles.Where(
                x => x.DateTime >= startDate && x.DateTime <= endDate
            );

            if (!string.IsNullOrEmpty(sitename))
            {
                posts = posts.Where(x => x.RssFeed.Name.ToLower() == sitename.ToLower());
            }

            var postsCount = posts.Count();

            posts = posts
                .OrderByDescending(x => x.DateTime)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            var postDTOs = posts
                .Select(
                    x =>
                        new RssPostDTO
                        {
                            DateTime = x.DateTime,
                            Description = x.Description,
                            Id = x.Id,
                            SiteName = x.RssFeed.Name,
                            Title = x.Title,
                            URL = x.URL,
                        }
                )
                .ToList();

            return new PagedResponse<IEnumerable<RssPostDTO>>(
                postDTOs,
                paginationFilter.PageNumber,
                paginationFilter.PageSize,
                postsCount
            );
        }

        public bool SavePostForLater(string userId, int postId)
        {
            var userSettings = _context.ApplicationUserSettings
                .Include(x => x.SavedArticles)
                .FirstOrDefault(x => x.UserId == userId);

            var post = _context.RssArticles.FirstOrDefault(x => x.Id == postId);

            var savedPosts = userSettings.SavedArticles.ToList();

            if (!savedPosts.Contains(post))
            {
                savedPosts.Add(post);
            }

            userSettings.SavedArticles = savedPosts;

            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool RemovePostForLater(string userId, int postId)
        {
            var userSettings = _context.ApplicationUserSettings
                .Include(x => x.SavedArticles)
                .FirstOrDefault(x => x.UserId == userId);

            var post = _context.RssArticles.FirstOrDefault(x => x.Id == postId);

            var savedPosts = userSettings.SavedArticles.ToList();

            savedPosts.Remove(post);

            userSettings.SavedArticles = savedPosts;

            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public PagedResponse<IEnumerable<RssPostDTO>> GetPostsByDateRangeForLater(
            PaginationFilter paginationFilter,
            string userId
        )
        {
            var userSettings = _context.ApplicationUserSettings
                .Include(x => x.SavedArticles)
                .FirstOrDefault(x => x.UserId == userId);

            var posts = userSettings.SavedArticles
                .OrderByDescending(x => x.DateTime)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            var postsCount = userSettings.SavedArticles.Count();

            var postsDto = posts.Select(
                x =>
                    new RssPostDTO
                    {
                        DateTime = x.DateTime,
                        Description = x.Description,
                        Id = x.Id,
                        IsSavedForLater = true,
                        SiteName = x.RssFeed.Name,
                        Title = x.Title,
                        URL = x.URL
                    }
            );

            return new PagedResponse<IEnumerable<RssPostDTO>>(
                postsDto,
                paginationFilter.PageNumber,
                paginationFilter.PageSize,
                postsCount
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Extensions;
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

        public ArticlesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedResponse<IEnumerable<RssArticleDTO>> GetArticlesByDateRange(
            PaginationFilter paginationFilter,
            DateTime startDate,
            DateTime endDate,
            int? feedId
        )
        {
            var articles = _context.RssArticles.Where(
                x => x.DateTime >= startDate && x.DateTime <= endDate
            );

            if (feedId != null)
            {
                articles = articles.Where(x => x.RssFeedId == feedId);
            }

            var articlesCount = articles.Count();

            var paginatedArticles = articles.Paginate(paginationFilter);

            var articleDTOs = paginatedArticles
                .Select(
                    x =>
                        new RssArticleDTO
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

            return new PagedResponse<IEnumerable<RssArticleDTO>>(
                articleDTOs,
                paginationFilter.PageNumber,
                paginationFilter.PageSize,
                articlesCount
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

        public PagedResponse<IEnumerable<RssArticleDTO>> GetPostsByDateRangeForLater(
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
                    new RssArticleDTO
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

            return new PagedResponse<IEnumerable<RssArticleDTO>>(
                postsDto,
                paginationFilter.PageNumber,
                paginationFilter.PageSize,
                postsCount
            );
        }
    }
}

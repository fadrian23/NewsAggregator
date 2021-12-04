using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAggregator.Services.Implementation
{
    public class RssSitesService : IRssSitesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RssSitesService> _logger;
        private readonly IPostCategorizationService _postCategorizationService;

        public RssSitesService(ApplicationDbContext context, ILogger<RssSitesService> logger, IPostCategorizationService postCategorizationService)
        {
            _context = context;
            _logger = logger;
            _postCategorizationService = postCategorizationService;
        }

        public PagedResponse<IEnumerable<RssPostDTO>> GetAllPosts(PaginationFilter paginationFilter)
        {
            var posts = _context.InformationSitesPosts
                                .OrderByDescending(x => x.DateTime)
                                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                                .Take(paginationFilter.PageSize)
                                .ToList();

            var postsCount = _context.InformationSitesPosts.Count();

            var postsDto = posts.Select(x => new RssPostDTO
            {
                DateTime = x.DateTime,
                Description = x.Description,
                Id = x.Id,
                SiteName = x.SiteName,
                Title = x.Title,
                URL = x.URL
            });

            return new PagedResponse<IEnumerable<RssPostDTO>>(postsDto, paginationFilter.PageNumber, paginationFilter.PageSize, postsCount);
        }

        public PagedResponse<IEnumerable<RssPostDTO>> GetAllPostsByDateRange(PaginationFilter paginationFilter, DateTime startDate, DateTime endDate)
        {
            var posts = _context.InformationSitesPosts
                                .Where(z => z.DateTime.Date >= startDate.Date && z.DateTime.Date <= endDate.Date)
                                .OrderByDescending(x => x.DateTime)
                                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                                .Take(paginationFilter.PageSize);

            var postsCount = _context.InformationSitesPosts
                                    .Where(z => z.DateTime.Date >= startDate.Date && z.DateTime.Date <= endDate.Date)
                                    .Count();


            var postsDto = posts.Select(x => new RssPostDTO
            {
                DateTime = x.DateTime,
                Description = x.Description,
                Id = x.Id,
                SiteName = x.SiteName,
                Title = x.Title,
                URL = x.URL
            });

            return new PagedResponse<IEnumerable<RssPostDTO>>(postsDto, paginationFilter.PageNumber, paginationFilter.PageSize, postsCount);


        }

        public PagedResponse<IEnumerable<RssPostDTO>> GetPosts(PaginationFilter paginationFilter, string sitename)
        {
            var posts = _context.InformationSitesPosts
                                .Where(x => x.SiteName.ToLower() == sitename.ToLower())
                                .OrderByDescending(x => x.DateTime)
                                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                                .Take(paginationFilter.PageSize);

            var postsCount = _context.InformationSitesPosts
                                     .Where(x => x.SiteName.ToLower() == sitename.ToLower())
                                     .Count();

            var postsDto = posts.Select(x => new RssPostDTO
            {
                DateTime = x.DateTime,
                Description = x.Description,
                Id = x.Id,
                SiteName = x.SiteName,
                Title = x.Title,
                URL = x.URL
            });

            return new PagedResponse<IEnumerable<RssPostDTO>>
                (postsDto, paginationFilter.PageNumber, paginationFilter.PageSize, postsCount);
        }

        public PagedResponse<IEnumerable<RssPostDTO>> GetPostsByDateRange(PaginationFilter paginationFilter, string sitename, DateTime startDate, DateTime endDate, string userId = null)
        {
            var posts = _context.InformationSitesPosts
                                .Where(x => x.SiteName.ToLower() == sitename.ToLower())
                                .Where(z => z.DateTime.Date >= startDate.Date && z.DateTime.Date <= endDate.Date)
                                .OrderByDescending(x => x.DateTime)
                                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                                .Take(paginationFilter.PageSize);


            var postsCount = _context.InformationSitesPosts
                                    .Where(x => x.SiteName.ToLower() == sitename.ToLower())
                                    .Where(z => z.DateTime.Date >= startDate.Date && z.DateTime.Date <= endDate.Date)
                                    .Count();

            var postsDto = posts.Select(x => new RssPostDTO
            {
                DateTime = x.DateTime,
                Description = x.Description,
                Id = x.Id,
                SiteName = x.SiteName,
                Title = x.Title,
                URL = x.URL,
                IsSavedForLater = !string.IsNullOrEmpty(userId) ? _context.ApplicationUserSettings.Include(a => a.SavedPosts)
                                            .FirstOrDefault(c => c.UserId == userId)
                                            .SavedPosts.Any(z => z.Id == x.Id) : false,
            });

            return new PagedResponse<IEnumerable<RssPostDTO>>(postsDto, paginationFilter.PageNumber, paginationFilter.PageSize, postsCount);
        }

        public void FetchDataFromRssFeed(string siteName, string URL)
        {
            string rss;
            _logger.LogInformation($"Fetching posts from {siteName}");

            System.Console.WriteLine(URL);
            using (var wc = new WebClient())
            {
                wc.Headers["User-Agent"] = "github.com/risenn23";
                rss = wc.DownloadString(URL);
            }
            XmlReader reader = XmlReader.Create(new StringReader(rss));
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            List<RssPost> items = new();

            items = GetRssPostList(feed, siteName);

            foreach (var item in items)
            {
                if (!_context.InformationSitesPosts.Any(x => x.URL == item.URL))
                {
                    _context.InformationSitesPosts.Add(item);
                }
            }


            if (_context.SaveChanges() > 0)
            {
                _logger.LogInformation($"found new posts on {siteName}");
            }
            else
            {
                _logger.LogInformation($"no new posts on {siteName}");
            }

        }

        private List<RssPost> GetRssPostList(SyndicationFeed feed, string siteName)
        {
            List<RssPost> posts = new List<RssPost>();

            foreach (var item in feed.Items)
            {
                switch (siteName)
                {
                    case "Onet":
                        {
                            posts.Add((RssPost)_postCategorizationService.CategorizePost(DeleteXmlTagsFromPostDescription(
                                new RssPost
                                {
                                    Title = item.Title.Text,
                                    SiteName = siteName,
                                    DateTime = item.PublishDate.DateTime,
                                    Description = item.Summary.Text,
                                    URL = "http://wiadomosci.onet.pl/" + item.Links[0].Uri.AbsolutePath,
                                }
                            )));
                            break;
                        }
                    case "WP":
                        {
                            posts.Add((RssPost)_postCategorizationService.CategorizePost(DeleteXmlTagsFromPostDescription(new RssPost
                            {
                                Title = item.Title.Text,
                                SiteName = siteName,
                                DateTime = item.PublishDate.DateTime,
                                Description = item.Summary.Text,
                                URL = "http://wiadomosci.wp.pl" + item.Links[0].Uri.AbsolutePath,
                            })));
                            break;
                        }
                    default:
                        {
                            posts.Add((RssPost)_postCategorizationService.CategorizePost(DeleteXmlTagsFromPostDescription(
                                new RssPost
                                {
                                    Title = item.Title.Text,
                                    SiteName = siteName,
                                    DateTime = item.PublishDate.DateTime,
                                    Description = item.Summary.Text,
                                    URL = item.Id
                                })));
                            break;
                        }
                }
            }

            return posts;
        }

        private RssPost DeleteXmlTagsFromPostDescription(RssPost post)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(post.Description);
            var description = htmlDocument.DocumentNode.InnerText.Trim();

            return new RssPost
            {
                DateTime = post.DateTime,
                Description = description,
                SiteName = post.SiteName,
                Title = post.Title,
                URL = post.URL
            };
        }

        public bool SavePostForLater(string userId, int postId)
        {
            var userSettings = _context.ApplicationUserSettings
                                        .Include(x => x.SavedPosts)
                                        .FirstOrDefault(x => x.UserId == userId);

            var post = _context.InformationSitesPosts.FirstOrDefault(x => x.Id == postId);

            var savedPosts = userSettings.SavedPosts.ToList();

            if (!savedPosts.Contains(post))
            {
                savedPosts.Add(post);
            }

            userSettings.SavedPosts = savedPosts;

            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool RemovePostForLater(string userId, int postId)
        {
            var userSettings = _context.ApplicationUserSettings.Include(x => x.SavedPosts).FirstOrDefault(x => x.UserId == userId);

            var post = _context.InformationSitesPosts.FirstOrDefault(x => x.Id == postId);

            var savedPosts = userSettings.SavedPosts.ToList();

            savedPosts.Remove(post);

            userSettings.SavedPosts = savedPosts;

            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}

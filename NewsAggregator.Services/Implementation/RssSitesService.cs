﻿using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
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

        public RssSitesService(ApplicationDbContext context, ILogger<RssSitesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public PagedResponse<IEnumerable<RssPost>> GetPosts(PaginationFilter paginationFilter, string sitename)
        {
            var posts = _context.InformationSitesPosts
                                .Where(x => x.SiteName.ToLower() == sitename.ToLower())
                                .OrderByDescending(x => x.Date)
                                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                                .Take(paginationFilter.PageSize);
            var postsCount = _context.InformationSitesPosts
                                     .Where(x => x.SiteName.ToLower() == sitename.ToLower())
                                     .Count();

            return new PagedResponse<IEnumerable<RssPost>>
                (posts, paginationFilter.PageNumber, paginationFilter.PageSize, postsCount);
        }

        public void FetchDataFromRssFeed(string siteName, string URL)
        {
            string rss;
            _logger.LogInformation($"Fetching posts from {siteName}");
            using (var wc = new WebClient())
            {
                wc.Headers["User-Agent"] = "github.com/risenn23";
                rss = wc.DownloadString(URL);
            }
            XmlReader reader = XmlReader.Create(new StringReader(rss));
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            List<RssPost> items = new();

            foreach (var item in feed.Items)
            {
                items.Add(new RssPost
                {
                    Title = item.Title.Text,
                    SiteName = siteName,
                    Date = item.PublishDate.DateTime,
                    Description = item.Summary.Text,
                    URL = item.Id
                });
            }

            // todo: figure out better way to check for duplicates
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
    }
}
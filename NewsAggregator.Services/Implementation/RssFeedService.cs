using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NewsAggregator.Services.Implementation
{
    public class RssFeedService : IRssFeedService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RssFeedService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public RssFeedService(
            ApplicationDbContext context,
            ILogger<RssFeedService> logger,
            IHttpClientFactory httpClientFactory
        )
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        private IEnumerable<RssPost> GetRssPostsFromRootXElement(
            XElement rootElement,
            string siteName
        )
        {
            var versionElement = rootElement.Attribute("version");

            var feedVersion = 1;

            if (versionElement is not null)
            {
                feedVersion = Convert.ToInt32(
                    Convert.ToDouble(versionElement.Value, CultureInfo.InvariantCulture)
                );
                rootElement = rootElement.Element("channel");
            }

            string description = "description";
            string date = "pubDate";
            string item = "item";
            var ns = rootElement.GetDefaultNamespace();

            IEnumerable<XElement> articles = rootElement.Elements(item);

            if (feedVersion != 2)
            {
                description = "summary";
                date = "published";
                item = "entry";
                articles = rootElement.Elements(ns + "entry");
            }

            return articles.Select(
                article =>
                    new RssPost
                    {
                        SiteName = siteName,
                        Description = RemoveHtmlTagsFromText(
                            article.Element(ns + description).Value
                        ),
                        Title = article.Element(ns + "title").Value,
                        URL = article.Element(ns + "link").Value,
                        DateTime = DateTime.Parse(article.Element(ns + date).Value)
                    }
            );
        }

        private string RemoveHtmlTagsFromText(string text)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(text);
            return doc.DocumentNode.InnerText;
        }

        public async Task<KeyValuePair<string, IEnumerable<RssPost>>> GetArticlesFromRssFeed(
            string siteName,
            string URL
        )
        {
            var httpClient = _httpClientFactory.CreateClient("RssFeed");
            var stream = await httpClient.GetStreamAsync(URL);

            XElement root = XElement.Load(stream);

            var articles = GetRssPostsFromRootXElement(root, siteName);

            return new KeyValuePair<string, IEnumerable<RssPost>>(siteName, articles);
        }

        public void SaveArticles(IEnumerable<RssPost> articles, string siteName)
        {
            var newArticles = articles.Where(
                article => !_context.InformationSitesPosts.Any(x => x.URL == article.URL)
            );

            _context.InformationSitesPosts.AddRange(newArticles);

            var newArticlesCount = _context.SaveChanges();

            if (newArticlesCount > 0)
            {
                _logger.LogInformation($"Found {newArticlesCount} new posts on {siteName}");
            }
        }

        private bool IsArticleSaved(string userId, int postId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            return _context.ApplicationUserSettings
                .Include(x => x.SavedPosts)
                .FirstOrDefault(x => x.UserId == userId)
                .SavedPosts.Any(x => x.Id == postId);
        }
    }
}

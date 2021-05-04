using NewsAggregator.Data.Models;
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
    public class RssFeedService : IRssFeedService
    {

        public IEnumerable<RssPost> GetData(string siteName, string URL)
        {
            string rss;
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

            return items;
        }
    }
}

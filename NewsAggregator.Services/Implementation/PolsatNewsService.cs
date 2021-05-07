using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class PolsatNewsService : ISiteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRssFeedService _rssFeedService;
        private readonly ILogger<PolsatNewsService> _logger;

        public PolsatNewsService(ApplicationDbContext context, IRssFeedService rssFeedService, ILogger<PolsatNewsService> logger)
        {
            _context = context;
            _rssFeedService = rssFeedService;
            _logger = logger;
        }

        public bool GetAndSaveData()
        {
            _logger.LogInformation("getting data from polsatnews");
            var data = _rssFeedService.GetData(AvailableSites.PolsatNews, "https://www.polsatnews.pl/rss/wszystkie.xml");
            foreach (var item in data)
            {
                _context.InformationSitesPosts.Add(item);
            }
            return _context.SaveChanges() > 0;
        }

        public PagedResponse<IEnumerable<ISocialModelDTO>> GetPosts(PaginationFilter paginationFilter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISocialModelDTO> GetPostsByDate(DateTime Date)
        {
            throw new NotImplementedException();
        }
    }
}

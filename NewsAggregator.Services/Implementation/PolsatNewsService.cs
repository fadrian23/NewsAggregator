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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    //public class PolsatNewsService : ISiteService
    //{
    //    private readonly ApplicationDbContext _context;
    //    private readonly IRssSitesService _rssFeedService;
    //    private readonly ILogger<PolsatNewsService> _logger;

    //    public PolsatNewsService(ApplicationDbContext context, IRssSitesService rssFeedService, ILogger<PolsatNewsService> logger)
    //    {
    //        _context = context;
    //        _rssFeedService = rssFeedService;
    //        _logger = logger;
    //    }

    //    public bool GetAndSaveData()
    //    {
    //        _logger.LogInformation("getting data from polsatnews");
    //        //var data = _rssFeedService.FetchDataFromRssFeed(AvailableSites.PolsatNews, "https://www.polsatnews.pl/rss/wszystkie.xml");
    //        foreach (var item in data)
    //        {
    //            _context.InformationSitesPosts.Add(item);
    //        }
    //        return _context.SaveChanges() > 0;
    //    }

    //    public PagedResponse<IEnumerable<ISocialModelDTO>> GetPosts(PaginationFilter paginationFilter)
    //    {
    //        IEnumerable<RssPostDTO> posts = _context.InformationSitesPosts
    //            .Where(x => x.SiteName == AvailableSites.PolsatNews)
    //            .OrderByDescending(x => x.Date)
    //            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
    //            .Take(paginationFilter.PageSize)
    //            .Select(x => new RssPostDTO
    //            {
    //                Id = x.Id,
    //                DateTime = x.Date,
    //                Description = x.Description,
    //                Title = x.Title,
    //                URL = x.URL,
    //                SiteName = x.SiteName
    //            });

    //        var postsCount = _context.InformationSitesPosts
    //            .Where(x => x.SiteName == AvailableSites.PolsatNews)
    //            .Count();

    //        return new PagedResponse<IEnumerable<ISocialModelDTO>>(posts, paginationFilter.PageNumber, paginationFilter.PageSize, postsCount);

    //    }

    //    public IEnumerable<ISocialModelDTO> GetPostsByDate(DateTime Date)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAggregator.Services.HelperModels;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Services.Services;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.Filters;

namespace NewsAggregator.Services.Implementation
{
    public class SitesService : ISitesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISiteFactory _siteFactory;

        public SitesService(ApplicationDbContext context, ISiteFactory siteFactory)
        {
            _context = context;
            _siteFactory = siteFactory;
        }

        public PagedResponse<IEnumerable<RssPostDTO>> GetPostsFromUserSites(string userId, PaginationFilter paginationFilter, DateTime startDate, DateTime endDate)
        {
            var userSites = _context.ApplicationUserSettings
                .Include(x => x.SiteNames)
                .FirstOrDefault(z => z.UserId == userId)
                .SiteNames.Select(xz => xz.Name.ToLower())
                .ToList();


            var RssPostDTOs = _context.InformationSitesPosts
                                .Where(x => userSites.Contains(x.SiteName.ToLower()))
                                .Where(x => x.DateTime.Date >= startDate.Date && x.DateTime.Date <= endDate.Date)
                                .OrderByDescending(x => x.DateTime)
                                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                                .Take(paginationFilter.PageSize);

            var postsCount = _context.InformationSitesPosts.Where(x => userSites.Contains(x.SiteName.ToLower()))
                                                            .Where(x => x.DateTime.Date >= startDate.Date && x.DateTime.Date <= endDate.Date)
                                                            .Count();

            var data = RssPostDTOs.Select(x => new RssPostDTO
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

            return new PagedResponse<IEnumerable<RssPostDTO>>(data, paginationFilter.PageNumber, paginationFilter.PageSize, postsCount);
        }

        public List<string> GetSubscribedSites(string userId)
        {
            var result = _context.ApplicationUserSettings.Include(x => x.SiteNames).FirstOrDefault(x => x.UserId == userId).SiteNames.Select(x => x.Name).ToList();
            return result;
        }

        public SiteSubscriptionResult SubscribeToSites(IEnumerable<string> sites, string userId)
        {
            var userSettings = _context.ApplicationUserSettings.Include(x => x.SiteNames).FirstOrDefault(z => z.UserId == userId);

            List<SiteName> siteNames = new();

            var result = new SiteSubscriptionResult
            {
                Success = false,
                Errors = new List<string>()
            };

            foreach (var site in sites)
            {
                var sitename = _context.SiteNames.FirstOrDefault(x => x.Name.ToLower() == site.ToLower());
                if (sitename != null)
                {
                    siteNames.Add(sitename);
                }
                else
                {
                    result.Errors.Add($"Could not find {site} in available sites.");
                }
            }

            if (result.Errors.Count > 0)
            {
                return result;
            }

            userSettings.SiteNames = siteNames;


            _context.SaveChanges();

            return new SiteSubscriptionResult
            {
                Success = true
            };
        }
    }
}

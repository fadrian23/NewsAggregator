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

        public UserSitesPostsDTO GetPostsFromUserSites(string userId, PaginationFilter paginationFilter)
        {
            var userSites = _context.ApplicationUserSettings
                .Include(x => x.SiteNames)
                .FirstOrDefault(z => z.UserId == userId)
                .SiteNames.Select(xz => xz.Name)
                .ToList();

            var userSitesPostsDTO = new UserSitesPostsDTO();

            foreach (var site in userSites)
            {
                if (AvailableSites.GetAll().Contains(site))
                {

                    var siteService = _siteFactory.For(site);
                    var filter = new PaginationFilter
                    {
                        PageNumber = paginationFilter.PageNumber,
                        PageSize = paginationFilter.PageSize
                    };

                    switch (site)
                    {
                        case AvailableSites.Reddit:
                            {
                                userSitesPostsDTO.RedditPosts = (IEnumerable<RedditPostDTO>)siteService.GetPosts(filter);
                                break;
                            }
                        case AvailableSites.HackerNews:
                            {
                                userSitesPostsDTO.HackerNewsPosts = (IEnumerable<HackerNewsPostDTO>)siteService.GetPosts(filter);
                                break;
                            }
                        default:
                            throw new NotImplementedException($"not implemented for {site}");
                    }
                }
            }



            return userSitesPostsDTO;
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

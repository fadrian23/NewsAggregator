﻿using Microsoft.Extensions.DependencyInjection;
using NewsAggregator.Services.Implementation;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRestService, RestService>();
            services.AddScoped<IRedditService, RedditService>();
            services.AddScoped<IHackerNewsService, HackerNewsService>();
            services.AddScoped<ISiteService, RedditService>();
            services.AddScoped<ISiteService, HackerNewsService>();
            services.AddScoped<ISitesService, SitesService>();
            services.AddScoped<ISiteFactory, SiteFactory>();
            services.AddScoped<RedditService>();
            services.AddScoped<HackerNewsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IScrapeJob, ScrapeJob>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}
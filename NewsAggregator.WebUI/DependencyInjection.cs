using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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


            services.AddScoped<ISitesService, SitesService>();
            services.AddScoped<IRssSitesService, RssSitesService>();

            services.AddScoped<IScrapeJob, ScrapeJob>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

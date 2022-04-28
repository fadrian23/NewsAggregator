using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsAggregator.Data.DatabaseContext;
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
            services.AddScoped<IRssFeedService, RssFeedService>();
            services.AddScoped<IArticlesService, ArticlesService>();

            services.AddScoped<IScrapeJob, ScrapeJob>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddHangfireAfterMigrations(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddHangfire(
                (serviceProvider, config) =>
                {
                    var context = serviceProvider
                        .CreateScope()
                        .ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    context.Database.Migrate();

                    config
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(
                            configuration.GetConnectionString("NewsAggregatorConnection"),
                            new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            }
                        );
                }
            );
            return services;
        }
    }
}

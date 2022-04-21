using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Data.Models;
using NewsAggregator.Data.Models.Identity;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Data.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ApplicationUserSettings> ApplicationUserSettings { get; set; }
        public DbSet<RssArticle> RssArticles { get; set; }
        public DbSet<RssFeed> RssFeeds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>().HasKey(k => k.Token);

            builder.Entity<RefreshToken>().Property(k => k.Token).ValueGeneratedOnAdd();

            builder
                .Entity<RssFeed>()
                .HasOne(x => x.ParentFeed)
                .WithMany(x => x.SubFeeds)
                .HasForeignKey(x => x.ParentFeedId);

            builder.Entity<RssFeed>().HasData(JsonSeedHelper.GetRssFeedData());
        }
    }
}

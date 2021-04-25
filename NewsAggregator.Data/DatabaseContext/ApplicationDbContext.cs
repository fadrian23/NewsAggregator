using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Data.Models;
using NewsAggregator.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Data.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<RedditPost> RedditPosts { get; set; }
        public DbSet<HackerNewsPost> HackerNewsPosts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SiteName> SiteNames { get; set; }
        public DbSet<ApplicationUserSettings> ApplicationUserSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>()
                .HasKey(k => k.Token);

            builder.Entity<RefreshToken>()
                .Property(k => k.Token)
                .ValueGeneratedOnAdd();

            builder.Entity<SiteName>()
                .HasData(
                new { Id = 1, Name = "Reddit" },
                new { Id = 2, Name = "HackerNews" }
                );

        }
    }

}


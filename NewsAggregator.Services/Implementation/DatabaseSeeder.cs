using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.Helpers;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(ApplicationDbContext context, ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void SeedDatabase()
        {
            _logger.LogInformation("Seeding db");

            var postCategories = GetPostCategoriesData();

            foreach (var postCategory in postCategories)
            {
                var postCategoryFromDb = _context.PostCategories
                    .Select(n => n.Name)
                    .FirstOrDefault(x => x == postCategory.Name);
                if (postCategoryFromDb == null)
                {
                    _context.PostCategories.Add(postCategory);
                }
            }
            _context.SaveChanges();
        }

        private IEnumerable<PostCategory> GetPostCategoriesData()
        {
            var postCategories = new List<PostCategory>();

            postCategories.Add(new PostCategory
            {
                Name = AvailableRedditSubreddits.Csharp,
                Keywords = new List<Keyword>
                {
                    new Keyword {Name = "csharp"}
                }
            });

            postCategories.Add(new PostCategory
            {
                Name = AvailableRedditSubreddits.Dotnet,
                Keywords = new List<Keyword>
                {
                    new Keyword {Name = "dotnet"}
                }
            });


            return postCategories;
        }
    }
}

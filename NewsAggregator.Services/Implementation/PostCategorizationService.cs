using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsAggregator.Data.DatabaseContext;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Implementation
{
    public class PostCategorizationService : IPostCategorizationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PostCategorizationService> _logger;

        public PostCategorizationService(ApplicationDbContext context, ILogger<PostCategorizationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IPost CategorizePost(IPost post)
        {
            var postCategoriesList = _context.PostCategories.Include(k => k.Keywords).ToList();
            foreach (var postCategory in postCategoriesList)
            {
                if (postCategory.Keywords.Any(s => post.Title.Contains(s.Name)))
                {
                    _logger.LogInformation("post contains keyword!" + post.Title);
                    post.PostCategory = postCategory;
                }
            }
            return post;
        }
    }
}

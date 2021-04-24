using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.DTOs;
using NewsAggregator.Services.Filters;

namespace NewsAggregator.Services.Services
{
    public interface ICategoryService
    {
        List<PostCategoryDTO> GetCategories(PaginationFilter paginationFilter);
        PostCategoryDTO GetCategory(int id);
        void AddCategory(PostCategory postCategory);
        bool EditCategory(int id, PostCategory postCategory);
        bool DeleteCategory(int id);
        List<KeywordDTO> GetCategoryKeywords(int id);
        bool AddKeywordsToCategory(List<Keyword> keywords, int postCategoryId);
        bool DeleteCategoryKeywords(int categoryId);
        CategoryPostsDTO GetPostsOfCategory(int categoryId, PaginationFilter paginationFilter);
        ISocialModel CategorizePost(ISocialModel post);

    }
}

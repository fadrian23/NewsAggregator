using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Data.Models;
using NewsAggregator.Services.Services;
using NewsAggregator.WebUI.Models.Requests;
using NewsAggregator.WebUI.Models.Responses;
using System.Linq;

namespace NewsAggregator.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // get api/category
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetCategories();

            if (categories is null)
            {
                return NotFound();
            }

            var response = categories.Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Category,
                Keywords = x.Keywords.ToList()
            }).ToList();

            return Ok(response);
        }

        // get api/category/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Category,
                Keywords = category.Keywords
            };


            return Ok(response);
        }

        // post api/category
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryRequest categoryRequest)
        {
            var postCategory = new PostCategory
            {
                Name = categoryRequest.Name,
                Keywords = categoryRequest.Keywords.Select(k => new Keyword
                {
                    Name = k,
                }
                ).ToList()
            };

            _categoryService.AddCategory(postCategory);

            var response = new CategoryResponse
            {
                Id = postCategory.Id,
                Name = postCategory.Name,
                Keywords = postCategory.Keywords.Select(keyword => keyword.Name).ToList()
            };

            return CreatedAtAction(nameof(GetCategory), new { postCategory.Id }, postCategory);
        }

        // put api/category/5
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCategory(int id, UpdateCategoryRequest categoryRequest)
        {
            if (_categoryService.GetCategory(id) == null)
            {
                return NotFound();
            }

            var postCategory = new PostCategory
            {
                Id = id,
                Name = categoryRequest.Name,
                Keywords = categoryRequest.Keywords.Select(k => new Keyword
                {
                    Name = k,
                }
                ).ToList()
            };

            _categoryService.EditCategory(id, postCategory);

            var response = new CategoryResponse
            {
                Id = id,
                Name = categoryRequest.Name,
                Keywords = categoryRequest.Keywords
            };

            return CreatedAtAction(nameof(GetCategory), new { response.Id }, response);
        }

        // delete api/category/5
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (_categoryService.DeleteCategory(id) == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        // get api/category/5/keywords
        [HttpGet]
        [Route("{id}/keywords")]
        public IActionResult GetCategoryKeywords(int id)
        {
            var keywords = _categoryService.GetCategoryKeywords(id);
            if (keywords == null)
            {
                return NotFound();
            }

            var response = new CategoryKeywordsResponse
            {
                Keywords = keywords
            };

            return Ok(response);
        }

        // post api/category/5/keywords
        [HttpPost]
        [Route("{categoryId}/addkeywords")]
        public IActionResult AddKeywordsToCategory(int categoryId, UpdateCategoryKeywordsRequest request)
        {
            if (_categoryService.GetCategory(categoryId) == null)
            {
                return NotFound();
            }

            var listOfKeywords = request.Keywords.Select(k => new Keyword
            {
                Name = k,
            }).ToList();

            _categoryService.AddKeywordsToCategory(listOfKeywords, categoryId);

            return RedirectToAction(nameof(GetCategoryKeywords), new { Id = categoryId });
        }

        // delete api/category/5/keywords
        [HttpDelete]
        [Route("{categoryId}/keywords")]
        public IActionResult DeleteCategoryKeywords(int categoryId)
        {

            if (_categoryService.DeleteCategoryKeywords(categoryId) == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // get api/category/5/posts
        [HttpGet]
        [Route("{categoryId}/posts")]
        public IActionResult GetPostsFromCategory(int categoryId)
        {
            var response = _categoryService.GetPostsOfCategory(categoryId);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.BusinessLayer.Abstract;
using Payment.DtoLayer.Dtos.CategoryDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("IsActiveAproved")]
        public IActionResult IsActiveAproved(int id)
        {
            _categoryService.TCategoryIsActiveChange(id);
            return Ok();
        }
        [HttpGet("IsActiveAprovedCancel")]
        public IActionResult IsActiveAprovedCancel(int id)
        {
            _categoryService.TCategoryIsActiveChangeCancel(id);
            return Ok();
        }
        [HttpGet]
        public IActionResult CategoryList()
        {
            var values = _categoryService.TGetList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category Category)
        {
            _categoryService.TInsert(Category);
            return Ok("Category added.");
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category Category)
        {
            _categoryService.TUpdate(Category);
            return Ok("Category updated.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            _categoryService.TDelete(value);
            return Ok("Category deleted.");
        }
        [HttpGet("GetCategories")]
        public IActionResult GetCategories(int page = 1, int pageSize = 5)
        {
            var categories = _categoryService.GetCategories(page, pageSize);
            var totalCategories = _categoryService.GetTotalCategories();

            var result = new
            {
                Categories = categories,
                TotalCategories = totalCategories,
                TotalPages = (int)Math.Ceiling((decimal)totalCategories / pageSize),
                CurrentPage = page
            };

            return Ok(result);
        }
    }
}

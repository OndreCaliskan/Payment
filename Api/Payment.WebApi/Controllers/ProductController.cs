using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.BusinessLayer.Abstract;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetLast3Product()
        {
            var values = _productService.TGetLast3Product();
            return Ok(values);
        }

        [HttpGet("GetProductWithCategoryNameById")]
        public IActionResult GetProductWithCategoryNameById(int id)
        {
            var values = _productService.TGetProductWithCategoryNameById(id);
            return Ok(values);
        }

        [HttpGet("GetTopThreeProductsByRating")]
        public IActionResult GetTopThreeProductsByRating()
        {
            var values = _productService.TGetTopThreeProductsByRating();
            return Ok(values);
        }
    }
}

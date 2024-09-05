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
        // Pagination ile ürünleri getirme
        [HttpGet("GetProducts")]
        public IActionResult GetProducts(int page = 1, int pageSize = 5)
        {
            var products = _productService.GetProducts(page, pageSize);
            var totalProducts = _productService.GetTotalProducts();

            var result = new
            {
                Products = products,
                TotalProducts = totalProducts,
                TotalPages = (int)Math.Ceiling((decimal)totalProducts / pageSize),
                CurrentPage = page
            };

            return Ok(result);
        }
    }
}

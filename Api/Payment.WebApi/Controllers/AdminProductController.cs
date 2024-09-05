using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.BusinessLayer.Abstract;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public AdminProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("IsActiveAproved")]
        public IActionResult IsActiveAproved(int id)
        {
            _productService.TProductIsActiveChange(id);
            return Ok();
        }
        [HttpGet("IsActiveAprovedCancel")]
        public IActionResult IsActiveAprovedCancel(int id)
        {
            _productService.TProductIsActiveChangeCancel(id);
            return Ok();
        }
        [HttpGet("GetProductWithCategoryName")]
        public IActionResult GetProductWithCategoryName()
        {
            var values = _productService.TGetProductWithCategoryName();
            return Ok(values);
        }

   



        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _productService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productService.TInsert(product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var values = _productService.TGetByID(id);
            _productService.TDelete(values);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            _productService.TUpdate(product);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var values = _productService.TGetByID(id);
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

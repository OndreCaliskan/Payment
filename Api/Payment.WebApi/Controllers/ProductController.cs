﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.BusinessLayer.Abstract;
using Payment.EntityLayer.Concrete;

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

        [HttpGet("GetLast3Product")]
        public IActionResult GetLast3Product()
        {
            var values = _productService.TGetLast3Product();
            return Ok(values);
        }
        //GetProductWithCategoryNameById

        [HttpGet("GetProductWithCategoryNameById")]
        public IActionResult GetProductWithCategoryNameById(int id)
        {
            var values = _productService.TGetProductWithCategoryNameById(id);
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

    }
}

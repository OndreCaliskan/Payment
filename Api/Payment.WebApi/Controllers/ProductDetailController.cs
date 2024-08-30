using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Payment.BusinessLayer.Abstract;
using Payment.DtoLayer.Dtos.ProductDetailDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;
        private readonly IMapper _mapper;

        public ProductDetailController(IProductDetailService productDetailService, IMapper mapper)
        {
            _productDetailService = productDetailService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductDetailList()
        {
            var values = _productDetailService.TGetList();
            var mappedValues = _mapper.Map<List<ResultProductDetailDto>>(values);
            return Ok(mappedValues);
        }

        //GetProductDetailByProductId

        [HttpGet("GetProductDetailByProductId")]
        public IActionResult GetProductDetailByProductId(int productId)
        {
            var value = _productDetailService.TGetProductDetailByProductId(productId);
            if (value == 0)
                return NotFound("ProductDetail Not Found.");

            return Ok(value);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductDetail(int id)
        {
            var value = _productDetailService.TGetByID(id);
            if (value == null)
                return NotFound("ProductDetail Not Found.");

            var mappedValue = _mapper.Map<ResultProductDetailDto>(value);
            return Ok(mappedValue);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = _productDetailService.TGetByID(createProductDetailDto.ProductID);
            if (existingProduct != null)
                return Conflict("A detail for this product already exists.");

            var value = _mapper.Map<ProductDetail>(createProductDetailDto);
            _productDetailService.TInsert(value);
            return Ok("ProductDetail added.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var value = _productDetailService.TGetByID(updateProductDetailDto.ProductDetailID);
            if (value == null)
                return NotFound("ProductDetail Not Found.");

            _mapper.Map(updateProductDetailDto, value);
            _productDetailService.TUpdate(value);

            return Ok("ProductDetail updated.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductDetail(int id)
        {
            var value = _productDetailService.TGetByID(id);
            if (value == null)
                return NotFound("ProductDetail Not Found.");

            _productDetailService.TDelete(value);
            return Ok("ProductDetail deleted.");
        }
    }
}

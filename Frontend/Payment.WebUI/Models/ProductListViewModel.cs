using Payment.DtoLayer.Dtos.ProductDtos;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.Models
{
    public class ProductListViewModel
    {
        public List<ProductDto> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

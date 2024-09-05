using Payment.DtoLayer.Dtos.CategoryDtos;
using Payment.DtoLayer.Dtos.ProductDtos;

namespace Payment.WebUI.Models
{
    public class CategoryListViewModel
    {
        public List<CategoryDto>? Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

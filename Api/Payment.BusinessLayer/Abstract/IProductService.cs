using Payment.DtoLayer.Dtos.ProductDtos;
using Payment.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        string TProductIsActiveChange(int id);
        string TProductIsActiveChangeCancel(int id);
        List<ProductDto> TGetProductWithCategoryName();
        List<ProductDto> TGetLast3Product();
        ProductDto TGetProductWithCategoryNameById(int id);
    }
}

using Payment.DtoLayer.Dtos.ProductDtos;
using Payment.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        string ProductIsActiveChange(int id);
        string ProductIsActiveChangeCancel(int id);
        List <ProductDto> GetProductWithCategoryName();
        List<ProductDto> GetLast3Product();
        ProductDto GetProductWithCategoryNameById(int id);
    }
}

using Payment.DtoLayer.Dtos.ProductDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        string ProductIsActiveChange(int id);
        string ProductIsActiveChangeCancel(int id);
        List <ProductDto> GetProductWithCategoryName();
        List<ProductDto> GetLast3Product();
        ProductDto GetProductWithCategoryNameById(int id);
        List<ProductDto> GetTopThreeProductsByRating();

        IEnumerable<ProductDto> GetProducts(int page, int pageSize);
        int GetTotalProducts();
    }
}

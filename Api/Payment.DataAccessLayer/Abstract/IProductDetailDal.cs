using Payment.DtoLayer.Dtos.ProductDetailDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.Abstract
{
    public interface IProductDetailDal : IGenericDal<ProductDetail>
    {
        int GetProductDetailByProductId(int productId);
    }
}

using Payment.EntityLayer.Concrete;

namespace Payment.BusinessLayer.Abstract
{
    public interface IProductDetailService: IGenericService<ProductDetail>
    {
        int TGetProductDetailByProductId(int productId);
    }
}

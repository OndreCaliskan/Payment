using Payment.BusinessLayer.Abstract;
using Payment.DataAccessLayer.Abstract;
using Payment.EntityLayer.Concrete;

namespace Payment.BusinessLayer.Concrete
{
    public class ProductDetailManager : IProductDetailService
    {
        private readonly IProductDetailDal _productDetailDal;

        public ProductDetailManager(IProductDetailDal productDetailDal)
        {
            _productDetailDal = productDetailDal;
        }

        public void TDelete(ProductDetail t)
        {
            _productDetailDal.Delete(t);
        }

        public ProductDetail TGetByID(int id)
        {
            return _productDetailDal.GetByID(id);
        }

        public List<ProductDetail> TGetList()
        {
            return _productDetailDal.GetList();
        }

        public int TGetProductDetailByProductId(int productId)
        {
            return _productDetailDal.GetProductDetailByProductId(productId);
        }

        public void TInsert(ProductDetail t)
        {
            _productDetailDal.Insert(t);
        }

        public void TUpdate(ProductDetail t)
        {
            _productDetailDal.Update(t);
        }
    }
}

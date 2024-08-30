using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.DataAccessLayer.Repositories;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.EntityFramework
{
    public class EfProductDetailDal : GenericRepository<ProductDetail>, IProductDetailDal
    {
        public EfProductDetailDal(Context context) : base(context)
        {
        }

        public int GetProductDetailByProductId(int productId)
        {
            var context = new Context();
            var productDetail = context.ProductDetails.Where(x => x.ProductID == productId).FirstOrDefault();
            if (productDetail != null)
            {
                return productDetail.ProductDetailID;
            }
            return 0;
        }
    }
}

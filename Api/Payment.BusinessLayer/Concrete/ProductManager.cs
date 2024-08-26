using Payment.BusinessLayer.Abstract;
using Payment.DataAccessLayer.Abstract;
using Payment.DtoLayer.Dtos.ProductDtos;
using Payment.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void TProductIsActiveChange(int id)
        {
            _productDal.ProductIsActiveChange(id);
        }
        public void TProductIsActiveChangeCancel(int id)
        {
            _productDal.ProductIsActiveChangeCancel(id);
        }
        public void TDelete(Product t)
        {
            _productDal.Delete(t);
        }

        

        public List<Product> TGetList()
        {
            return _productDal.GetList();

        }

        public void TInsert(Product t)
        {
            _productDal.Insert(t);
        }

        public void TUpdate(Product t)
        {
            _productDal.Update(t);
        }

        public Product TGetByID(int id)
        {
            return _productDal.GetByID(id);
        }

        public List<ProductDto> TGetProductWithCategoryName()
        {
            var values=_productDal.GetProductWithCategoryName();
            return values;
        }
    }
}

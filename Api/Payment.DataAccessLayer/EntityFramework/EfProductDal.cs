using Microsoft.EntityFrameworkCore;
using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.DataAccessLayer.Repositories;
using Payment.DtoLayer.Dtos.ProductDtos;
using Payment.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public EfProductDal(Context context) : base(context)
        {

        }

        public List<ProductDto> GetProductWithCategoryName()
        {
            var context = new Context();
            var values = context.Products.Include(x => x.Category).Select(y => new ProductDto
            {
                ProductID = y.ProductID,
                CategoryName = y.Category.Name,
                CategoryId = y.Category.Id, 
                Title=y.Title,
                Description=y.Description,
                Price=y.Price,
                DiscountRate=y.DiscountRate,
                Stock=y.Stock,
                CoverImage=y.CoverImage,
                Rating=y.Rating,
                IsActive=y.IsActive,
                CreateTime=y.CreateTime,
                UpdateTime=y.UpdateTime,
                CreateUser=y.CreateUser,
                UpdateUser=y.UpdateUser,


            });
            return values.ToList();
        }

        public void ProductIsActiveChange(int id)
        {
            var context = new Context();
            var values = context.Products.Find(id);
            values.IsActive = "Aktif";
            context.SaveChanges();
        }
        public void ProductIsActiveChangeCancel(int id)
        {
            var context = new Context();
            var values = context.Products.Find(id);
            values.IsActive = "Pasif";
            context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.DataAccessLayer.Repositories;
using Payment.DtoLayer.Dtos.CategoryDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.EntityFramework
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly Context _context;
        public EfCategoryDal(Context context) : base(context)
        {
            _context = context;
        }

        public string CategoryIsActiveChange(int id)
        {
            var context = new Context();
            var values = context.Categories.Find(id);
            values.IsActive = true;
            context.SaveChanges();
            if (values.IsActive == true)
            {
                return "Aktif";
            }
            return "Pasif";

        }

        public string CategoryIsActiveChangeCancel(int id)
        {
            var context = new Context();
            var values = context.Categories.Find(id);
            values.IsActive = false;
            context.SaveChanges();
            if (values.IsActive == false)
            {
                return "Pasif";
            }

            return "Aktif";
        }

        public IEnumerable<CategoryDto> GetCategories(int page, int pageSize)
        {
            // Sayfalama işlemi için Skip ve Take kullanacağız
            return _context.Categories
                .Skip((page - 1) * pageSize) // Sayfa başına alınacak kayıtları atlar
                .Take(pageSize) // İstenen sayıda kayıt alır
                .Select(category => new CategoryDto // Category'den CategoryDto'ya mapleme
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    IsActive = category.IsActive,
                    CreateTime = category.CreateTime,
                    UpdateTime = category.UpdateTime,
                    ImagePath = category.ImagePath,
                    CreateUser = category.CreateUser,
                    UpdateUser = category.UpdateUser
                })
                .ToList();
        }

        public int GetTotalCategories()
        {
            return _context.Categories.Count();
        }
    }
}
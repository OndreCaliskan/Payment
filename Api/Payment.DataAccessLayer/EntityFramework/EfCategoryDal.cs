using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.DataAccessLayer.Repositories;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.EntityFramework
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(Context context) : base(context)
        {
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

    }
}
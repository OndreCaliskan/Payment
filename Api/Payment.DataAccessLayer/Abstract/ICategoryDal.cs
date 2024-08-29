using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        string CategoryIsActiveChange(int id);
        string CategoryIsActiveChangeCancel(int id);
    }
}

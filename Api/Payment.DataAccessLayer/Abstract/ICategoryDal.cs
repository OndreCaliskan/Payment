using Payment.DtoLayer.Dtos.CategoryDtos;
using Payment.DtoLayer.Dtos.ProductDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        string CategoryIsActiveChange(int id);
        string CategoryIsActiveChangeCancel(int id);

        IEnumerable<CategoryDto> GetCategories(int page, int pageSize);
        int GetTotalCategories();
    }
}

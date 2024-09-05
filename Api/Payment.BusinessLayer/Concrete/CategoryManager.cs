using Payment.BusinessLayer.Abstract;
using Payment.DataAccessLayer.Abstract;
using Payment.DtoLayer.Dtos.CategoryDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void TDelete(Category t)
        {
            _categoryDal.Delete(t);
        }

        public Category TGetByID(int id)
        {
            return _categoryDal.GetByID(id);
        }

        public List<Category> TGetList()
        {
            return _categoryDal.GetList();
        }

        public void TInsert(Category t)
        {
            _categoryDal.Insert(t);
        }

        public string TCategoryIsActiveChange(int id)
        {
            return _categoryDal.CategoryIsActiveChange(id);
        }

        public string TCategoryIsActiveChangeCancel(int id)
        {
            return _categoryDal.CategoryIsActiveChangeCancel(id);
        }

        public void TUpdate(Category t)
        {
            _categoryDal.Update(t);
        }

        public IEnumerable<CategoryDto> GetCategories(int page, int pageSize)
        {
            return _categoryDal.GetCategories(page, pageSize);
        }

        public int GetTotalCategories()
        {
            return _categoryDal.GetTotalCategories();
        }
    }
}
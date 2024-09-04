using Payment.BusinessLayer.Abstract;
using Payment.EntityLayer.Concrete;
using Payment.WebUI.DTOs.CategoryDtos;

namespace Payment.WebUI.Services
{
    public class CategoryServiceUI : ICategoryServiceUI
    {
        private readonly ICategoryService _categoryService;

        public CategoryServiceUI(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void AddCategory(CreateCategoryDto createCategoryDto)
        {
            var value = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description,
                ImagePath = createCategoryDto.ImagePath,
                IsActive = createCategoryDto.IsActive,
                CreateTime = createCategoryDto.CreateTime,
                UpdateTime = createCategoryDto.UpdateTime,
                CreateUser = createCategoryDto.CreateUser,
                UpdateUser = createCategoryDto.UpdateUser
            };
            _categoryService.TInsert(value);
        }

        public void DeleteCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            if (value != null)
            {
                _categoryService.TDelete(value);
            }
        }

        public UpdateCategoryDto GetCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            return new UpdateCategoryDto
            {
                Name = value.Name,
                Description = value.Description,
                ImagePath = value.ImagePath,
                IsActive = value.IsActive,
                CreateTime = value.CreateTime,
                UpdateTime = value.UpdateTime,
                CreateUser = value.CreateUser,
                UpdateUser = value.UpdateUser
            };
        }

        public List<ResultCategoryDto> GetCategoryList()
        {
            return _categoryService.TGetList().Select(x => new ResultCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath = x.ImagePath,
                IsActive = x.IsActive,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime,
                CreateUser = x.CreateUser,
                UpdateUser = x.UpdateUser,
            }).ToList();
        }

        public void UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var value=_categoryService.TGetByID(updateCategoryDto.Id);
            if (value != null)
            {
                value.Name = updateCategoryDto.Name;
                value.Description = updateCategoryDto.Description;
                value.ImagePath = updateCategoryDto.ImagePath;
                value.IsActive = updateCategoryDto.IsActive;
                value.CreateTime = updateCategoryDto.CreateTime;
                value.UpdateTime = updateCategoryDto.UpdateTime;
                value.CreateUser = updateCategoryDto.CreateUser;
                value.UpdateUser = updateCategoryDto.UpdateUser;
                _categoryService.TUpdate(value);
            }
        }
    }
}

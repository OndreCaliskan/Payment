using Payment.WebUI.DTOs.CategoryDtos;

namespace Payment.WebUI.Services
{
    public interface ICategoryServiceUI
    {
        List<ResultCategoryDto> GetCategoryList();
        void AddCategory(CreateCategoryDto createCategoryDto);
        void UpdateCategory(UpdateCategoryDto updateCategoryDto);
        void DeleteCategory(int id);
        UpdateCategoryDto GetCategory(int id);

    }
}

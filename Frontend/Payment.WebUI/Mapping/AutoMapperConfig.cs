using AutoMapper;
using Payment.EntityLayer.Concrete;
using Payment.WebUI.DTOs.CategoryDtos;

namespace Payment.WebUI.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ResultCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();



        }
    }
}

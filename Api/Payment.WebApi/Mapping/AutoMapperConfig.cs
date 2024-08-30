using AutoMapper;
using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.DtoLayer.Dtos.ProductDetailDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Address, CreateAddressDto>().ReverseMap();
            CreateMap<Address,UpdateAddressDto>().ReverseMap();

            CreateMap<ProductDetail,CreateProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail,ResultProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail,UpdateProductDetailDto>().ReverseMap();
        }
    }
}

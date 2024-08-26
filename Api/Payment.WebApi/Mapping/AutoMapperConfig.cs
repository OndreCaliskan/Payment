using AutoMapper;
using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Address, CreateAddressDto>().ReverseMap();
            CreateMap<Address,UpdateAddressDto>().ReverseMap();
        }
    }
}

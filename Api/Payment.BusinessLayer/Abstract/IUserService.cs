using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.BusinessLayer.Abstract
{
    public interface IUserService:IGenericService<AppUser>
    {
        Task<List<ResultAddressDto>> GetAddresses(int id);
    }
}

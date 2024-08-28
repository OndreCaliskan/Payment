using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.Abstract;

public interface IUserDal:IGenericDal<AppUser>
{
    Task<List<ResultAddressDto>> GetAddresses(int id);
}

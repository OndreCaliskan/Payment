using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.Abstract
{
    public interface IAddressDal : IGenericDal<Address>
    {
        List<AddressWithUsernameDto> AddressWithUsername();
    }
}

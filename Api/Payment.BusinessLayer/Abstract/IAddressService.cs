using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.BusinessLayer.Abstract
{
    public interface IAddressService:IGenericService<Address>
    {
        List<AddressWithUsernameDto> TAddressWithUsername();
    }
}

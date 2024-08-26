using Payment.BusinessLayer.Abstract;
using Payment.DataAccessLayer.Abstract;
using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.BusinessLayer.Concrete
{
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        public List<AddressWithUsernameDto> TAddressWithUsername()
        {
            return _addressDal.AddressWithUsername();
        }

        public void TDelete(Address t)
        {
            _addressDal.Delete(t);
        }

        public Address TGetByID(int id)
        {
            return _addressDal.GetByID(id);
        }

        public List<Address> TGetList()
        {
            return _addressDal.GetList();
        }

        public void TInsert(Address t)
        {
            _addressDal.Insert(t);
        }

        public void TUpdate(Address t)
        {
            _addressDal.Update(t);
        }
    }
}

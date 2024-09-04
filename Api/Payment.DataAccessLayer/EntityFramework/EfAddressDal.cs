using Microsoft.EntityFrameworkCore;
using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.DataAccessLayer.Repositories;
using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.EntityFramework
{
    public class EfAddressDal : GenericRepository<Address>, IAddressDal
    {
        public EfAddressDal(Context context) : base(context)
        {
        }

        public List<AddressWithUsernameDto> AddressWithUsername()
        {
            var context= new Context();
            //var values = context.Addresses.Include(x => x.AppUser).Select(x => new AddressWithUsernameDto
            //{
            //    UserName = $"{x.AppUser.Name} {x.AppUser.Surname}",
            //    AddressID = x.AddressID,
            //    AddressLine = x.AddressLine,
            //    City = x.City,
            //    District = x.District,
            //    CreateTime = x.CreateTime,
            //    UpdateTime = x.UpdateTime,
            //    CreateUser = x.CreateUser,
            //    UpdateUser = x.UpdateUser
            //}).ToList();
            return new List<AddressWithUsernameDto>{ };
        }
    }
}

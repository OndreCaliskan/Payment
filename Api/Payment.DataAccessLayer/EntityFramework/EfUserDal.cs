using Microsoft.EntityFrameworkCore;
using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.DataAccessLayer.Repositories;
using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.EntityFramework;

public class EfUserDal : GenericRepository<AppUser>, IUserDal
{
    public EfUserDal(Context context) : base(context)
    {
    }

    public async Task<List<ResultAddressDto>> GetAddresses(int i)
    {
        var context = new Context();
        var user = await context.Users.Where(x => x.Id == i).Include(x => x.Addresses).FirstOrDefaultAsync();

        var addresses = user.Addresses.Select(x => new ResultAddressDto
        {
            AddressID = x.AddressID,
            AddressLine = x.AddressLine,
            City = x.City,
            Country = x.Country,
            PostalCode = x.PostalCode,
            CreateTime = x.CreateTime,
            UpdateTime = x.UpdateTime,
            CreateUser = x.CreateUser,
            UpdateUser = x.UpdateUser,
            AppUserId = x.AppUserId
        }).ToList();
        return addresses;
    }
}
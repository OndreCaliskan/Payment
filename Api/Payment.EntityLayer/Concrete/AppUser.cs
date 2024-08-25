using Microsoft.AspNetCore.Identity;
using Payment.EntityLayer.Concrete;

public class AppUser :IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public List<Address> Addresses { get; set; }
}

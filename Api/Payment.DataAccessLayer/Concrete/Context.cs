using Microsoft.EntityFrameworkCore;
using Payment.EntityLayer.Concrete;

namespace Payment.DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=94.199.202.242;Database=hakanhak_PRODUCT_SALE;User Id=PRODUCT; Password=Hakan.12328; TrustServerCertificate=True;");
        }

        public DbSet<Category> Categories { get; set; }
    }
}

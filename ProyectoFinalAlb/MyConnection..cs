
using Microsoft.EntityFrameworkCore;

namespace CleaningProductsApp
{
    public class MyConnection : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Server=localhost;Database=CleaningProductsDB;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}

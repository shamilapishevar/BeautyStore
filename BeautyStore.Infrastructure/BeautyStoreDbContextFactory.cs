using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BeautyStore.Infrastructure.Persistence;

namespace BeautyStore.Infrastructure
{
    public class BeautyStoreDbContextFactory : IDesignTimeDbContextFactory<BeautyStoreDbContext>
    {
        public BeautyStoreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BeautyStoreDbContext>();

            // آدرس دیتابیس رو همین‌جا تنظیم کن
            optionsBuilder.UseSqlServer("Server=.;Database=BeautyStoreDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new BeautyStoreDbContext(optionsBuilder.Options);
        }
    }
}

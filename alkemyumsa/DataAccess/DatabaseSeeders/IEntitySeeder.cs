using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.DatabaseSeeders
{
    public interface IEntitySeeder
    {
        void SeedDatabase(ModelBuilder modelBuilder);
    }
}

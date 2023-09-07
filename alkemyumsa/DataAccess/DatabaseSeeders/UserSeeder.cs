using alkemyumsa.Entities;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.DatabaseSeeders
{
    public class UserSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().HasData(
                new Usuarios { 
                    Id = 1,
                    Nombre = "Francisco",
                    Apellido = "Andreoli",
                    Email = "franandreoli7@gmail.com",
                    Contrasena  = "123456"
                    }
                );
        }
    }
}

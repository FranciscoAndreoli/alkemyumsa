using alkemyumsa.Entities;
using alkemyumsa.Helpers;
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
                    Contrasena  = PasswordHashHelper.EncryptPassword("123456", "franandreoli7@gmail.com"),
                    Rol = Roles.Administrador
                    }
                
                );
        }
    }
}

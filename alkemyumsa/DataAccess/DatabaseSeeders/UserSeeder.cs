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
                    Dni = 42641623,
                    Email = "franandreoli7@gmail.com",
                    Contrasena  = PasswordHashHelper.EncryptPassword("123456", "franandreoli7@gmail.com"),
                    Rol = Roles.Administrador
                    },
                new Usuarios
                {
                    Id = 2,
                    Nombre = "José",
                    Dni = 38543765,
                    Email = "josé@gmail.com",
                    Contrasena = PasswordHashHelper.EncryptPassword("123456", "josé@gmail.com"),
                    Rol = Roles.Consultor
                }

                );
        }
    }
}

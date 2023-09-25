using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.DatabaseSeeders
{
    public class ServicioSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicios>().HasData(
                new Servicios
                {
                    CodServicio = 1,
                    Descr = "Desarrollo y mantenimiento de sistemas de información para gestión petrolera.",
                    Estado = true,
                    ValorHora = 190000,
                    DeletedAt = null
                    
                },
                new Servicios
                {
                    CodServicio = 2,
                    Descr = "Instalación de plataformas petroleras en alta mar.",
                    Estado = true,
                    ValorHora = 1450000,
                    DeletedAt = null

                },
                new Servicios
                {
                    CodServicio = 3,
                    Descr = "Perforación exploratoria en terrenos offshore.",
                    Estado = true,
                    ValorHora = 2200000,
                    DeletedAt = null

                },
                new Servicios
                {
                    CodServicio = 4,
                    Descr = "Capacitaciones y entrenamientos en seguridad industrial petrolera.",
                    Estado = false,
                    ValorHora = 170000,
                    DeletedAt = null

                }

                );
        }
    }
}

using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.DatabaseSeeders
{
    public class ProyectoSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proyectos>().HasData(
                new Proyectos
                {
                    CodProyecto = 1,
                    Nombre = "Sistema de información TechOil",
                    Direccion = "Sector Montañoso El Olimpo, Bogotá.",
                    Estado = "Pendiente",
                    DeletedAt = null

                },
                new Proyectos
                {
                    CodProyecto = 2,
                    Nombre = "Plataforma petrolera",
                    Direccion = "Base Naval Atlántica, Buenos Aires.",
                    Estado = "Confirmado",
                    DeletedAt = null

                },
                new Proyectos
                {
                    CodProyecto = 3,
                    Nombre = "Perforación petrolera",
                    Direccion = "Parque Científico Gaia, Santiago.",
                    Estado = "Confirmado",
                    DeletedAt = null

                },
                new Proyectos
                {
                    CodProyecto = 4,
                    Nombre = "Capacitación de personal",
                    Direccion = "Torre Empresarial Orion, Piso 20, Lima.",
                    Estado = "Terminado",
                    DeletedAt = null

                }

                );
        }
    }
}

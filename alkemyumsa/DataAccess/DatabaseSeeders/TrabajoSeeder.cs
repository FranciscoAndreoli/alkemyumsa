using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.DatabaseSeeders
{
    public class TrabajoSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trabajos>().HasData(
                new Trabajos
                {
                    CodTrabajo = 1,
                    CodProyecto = 1,
                    CodServicio = 1,
                    Fecha = new DateTime(2022, 6, 2),
                    CantHoras = 110,
                    ValorHora = 190000,
                    Costo = 20900000,
                    DeletedAt = null

                },
                new Trabajos
                {
                    CodTrabajo = 2,
                    CodProyecto = 2,
                    CodServicio = 2,
                    Fecha = new DateTime(2022, 7, 3),
                    CantHoras = 200,
                    ValorHora = 1450000,
                    Costo = 290000000,
                    DeletedAt = null

                },
                new Trabajos
                {
                    CodTrabajo = 3,
                    CodProyecto = 3,
                    CodServicio = 3,
                    Fecha = new DateTime(2022, 8, 4),
                    CantHoras = 300,
                    ValorHora = 2200000,
                    Costo = 660000000,
                    DeletedAt = null

                },
                new Trabajos
                {
                    CodTrabajo = 4,
                    CodProyecto = 4,
                    CodServicio = 4,
                    Fecha = new DateTime(2022, 9, 5),
                    CantHoras = 24,
                    ValorHora = 170000,
                    Costo = 4080000,
                    DeletedAt = null

                }

                );
        }
    }
}

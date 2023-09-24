using Microsoft.EntityFrameworkCore;
using alkemyumsa.Entities;
using alkemyumsa.DataAccess.DatabaseSeeders;

namespace alkemyumsa.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        //base es el constructor de la clase DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Usuarios> Usuario { get; set; } // cada propiedad DbSet<T> representa una tabla en la base de datos. Puedo realizar todo tipo de operaciones CRUD.
        public DbSet<Trabajos> Trabajo { get; set; }
        public DbSet<Servicios> Servicio { get; set; }
        public DbSet<Proyectos> Proyecto { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)//Se utiliza para configurar aspectos de la base de datos (tablas, índices, claves primarias, etc)
        {
            var seeders = new List<IEntitySeeder>
                {
                    new UserSeeder(),
                    new ServicioSeeder(),
                    new ProyectoSeeder(),
                    new TrabajoSeeder()
                };
                

            foreach (var seeder in seeders)
            {
                seeder.SeedDatabase(modelBuilder);

            }
            base.OnModelCreating(modelBuilder);
        }
    }
}


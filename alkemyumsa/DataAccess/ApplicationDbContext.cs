using Microsoft.EntityFrameworkCore;
using alkemyumsa.Entities;
using alkemyumsa.DataAccess.DatabaseSeeders;

namespace alkemyumsa.DataAccess
{
    /// <summary>
    /// Representa el contexto de la base de datos para la aplicación, proporcionando acceso y gestión de las entidades.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia del contexto con las opciones especificadas.
        /// </summary>
        /// <param name="options">Opciones para configurar el contexto.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Representa la tabla de Usuarios en la base de datos.
        /// </summary>
        public DbSet<Usuarios> Usuario { get; set; }

        /// <summary>
        /// Representa la tabla de Trabajos en la base de datos.
        /// </summary>
        public DbSet<Trabajos> Trabajo { get; set; }

        /// <summary>
        /// Representa la tabla de Servicios en la base de datos.
        /// </summary>
        public DbSet<Servicios> Servicio { get; set; }

        /// <summary>
        /// Representa la tabla de Proyectos en la base de datos.
        /// </summary>
        public DbSet<Proyectos> Proyecto { get; set; }

        /// <summary>
        /// Configura el modelo de entidad-relación al crear la estructura de la base de datos.
        /// Se invoca típicamente al inicio para definir y configurar el modelo.
        /// </summary>
        /// <param name="modelBuilder">Constructs the model for a given context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seeders = new List<IEntitySeeder>
                {
                    new UserSeeder(),
                    new ServicioSeeder(),
                    new ProyectoSeeder(),
                    new TrabajoSeeder()
                };

            // Aplica todos los seeders para inicializar datos en la base de datos.
            foreach (var seeder in seeders)
            {
                seeder.SeedDatabase(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}

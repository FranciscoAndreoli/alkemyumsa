using alkemyumsa.DataAccess.Repositories;
using alkemyumsa.DataAccess;

namespace alkemyumsa.Services
{
    /// <summary>
    /// Clase que facilita la implementación del patrón Unit of Work.
    /// Proporciona una manera de trabajar con múltiples repositorios a la vez y garantiza que las operaciones se completen de manera atómica.
    /// </summary>
    public class UnitOfWorkService : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Repositorio para la gestión de usuarios.
        /// </summary>
        public UserRepository UserRepository { get; private set; }

        /// <summary>
        /// Repositorio para la gestión de proyectos.
        /// </summary>
        public ProjectRepository ProjectRepository { get; private set; }

        /// <summary>
        /// Repositorio para la gestión de trabajos.
        /// </summary>
        public WorkRepository WorkRepository { get; private set; }

        /// <summary>
        /// Repositorio para la gestión de servicios.
        /// </summary>
        public ServiceRepository ServiceRepository { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase UnitOfWorkService con el contexto proporcionado.
        /// También inicializa los repositorios que estarán disponibles a través de esta unidad de trabajo.
        /// </summary>
        /// <param name="context">Contexto de base de datos a utilizar.</param>
        public UnitOfWorkService(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            ProjectRepository = new ProjectRepository(_context);
            WorkRepository = new WorkRepository(_context);
            ServiceRepository = new ServiceRepository(_context);
        }

        /// <summary>
        /// Guarda todos los cambios realizados en el contexto de la base de datos.
        /// </summary>
        /// <returns>Número de registros afectados.</returns>
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Libera los recursos utilizados por el contexto de la base de datos.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

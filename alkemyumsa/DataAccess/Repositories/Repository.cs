using log4net.Core;
using alkemyumsa.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace alkemyumsa.DataAccess.Repositories
{
    /// <summary>
    /// Representa un repositorio genérico para la entidad T.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad gestionada por el repositorio.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inicializa una nueva instancia de la clase Repository con el contexto especificado.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las entidades de tipo T.
        /// </summary>
        /// <returns>Una lista de entidades de tipo T.</returns>
        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Obtiene una entidad de tipo T por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>Una entidad de tipo T si es encontrada; de lo contrario, null.</returns>
        public virtual Task<T?> Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta una nueva entidad de tipo T.
        /// </summary>
        /// <param name="entity">Entidad a insertar.</param>
        /// <returns>True si la inserción es exitosa, de lo contrario false.</returns>
        public virtual async Task<bool> Insert(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                return true;
            }
            catch (Exception) { return false; }   
        }

        /// <summary>
        /// Actualiza una entidad de tipo T.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        /// <returns>True si la actualización es exitosa, de lo contrario false.</returns>
        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina una entidad de tipo T por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>True si la eliminación es exitosa, de lo contrario false.</returns>
        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifica una entidad basada en los datos proporcionados.
        /// </summary>
        /// <param name="data">Datos para realizar la verificación.</param>
        /// <returns>True si los datos coinciden con alguna entidad, de lo contrario false.</returns>
        public virtual Task<bool> Check(string data)
        {
            throw new NotImplementedException();
        }
    }
}

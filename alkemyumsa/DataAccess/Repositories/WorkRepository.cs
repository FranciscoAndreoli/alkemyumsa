using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    /// <summary>
    /// Provee métodos para el acceso a datos de Trabajos.
    /// </summary>
    public class WorkRepository : Repository<Trabajos>, IWorkRepository
    {
        /// <summary>
        /// Inicializa una nueva instancia de  <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="context">Context actúa como una sesión entre la aplicación y la base de datos.</param>
        public WorkRepository(ApplicationDbContext context) : base(context)
        { }

        
        /// <summary>
        /// Devuelve los Trabajos no eliminados.
        /// </summary>
        /// <returns>Una lista de los Trabajos activos.</returns>
        public async override Task<List<Trabajos>> GetAll()
        {
            try
            {
                return await _context.Trabajo
                    .Include(t => t.Proyecto)
                    .Include(t => t.Servicio)
                    .Where(t => t.DeletedAt == null)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                // For example, if using ILogger: _logger.LogError(ex, "Error fetching trabajos");
                // Depending on your use-case, you might want to throw the exception, return an empty list, or handle it differently.
                throw;
            }
        }

        /// <summary>
        /// Devuelve un trabajo según su ID.
        /// </summary>
        /// <param name="id">El ID del trabajo a devolver.</param>
        /// <returns>El trabajo. Sino, devuelve null.</returns>
        public async override Task<Trabajos?> Get(int id)
        {
            try
            {
                return await _context.Trabajo
                    .Include(t => t.Proyecto)
                    .Include(t => t.Servicio)
                    .Where(t => t.CodTrabajo == id)
                    .Where(t => t.DeletedAt == null)
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Actualiza la información de un trabajo.
        /// </summary>
        /// <param name="updateTrabajo">El DTO de trabajo.</param>
        /// <returns>True si se actualizó correctamente. Sino, false.</returns>
        public async override Task<bool> Update(Trabajos updateTrabajo)
        {
            try
            {
                var trabajo = await _context.Trabajo.SingleOrDefaultAsync(x => x.CodTrabajo == updateTrabajo.CodTrabajo);
                if (trabajo == null || trabajo.DeletedAt != null) { return false; }

                trabajo.Fecha = updateTrabajo.Fecha; // yy-mm-dd
                trabajo.CantHoras = updateTrabajo.CantHoras;
                trabajo.ValorHora = updateTrabajo.ValorHora;
                trabajo.Costo = updateTrabajo.CantHoras * updateTrabajo.ValorHora;
                trabajo.CodProyecto = updateTrabajo.CodProyecto;
                trabajo.CodServicio = updateTrabajo.CodServicio;

                _context.Trabajo.Update(trabajo);

                return true;
            }
            catch (Exception ex) { throw; }
        }
        /// <summary>
        /// Borrado lógico de trabajo.
        /// </summary>
        /// <param name="id">ID del trabajo a eliminar.</param>
        /// <returns>True si el trabajo fue encontrado. Sino, false.</returns>
        public async override Task<bool> Delete(int id)
        {
            try
            {
                var trabajo = await _context.Trabajo.Where(x => x.CodTrabajo == id).SingleOrDefaultAsync();
                if (trabajo != null && trabajo.DeletedAt == null)
                {
                    //_context.trabajo.Remove(user); // borrado físico
                    trabajo.DeletedAt = DateTime.UtcNow; // borrado lógico

                    return true;
                }
                return false;
            }catch (Exception ex) { throw; }

        }       
    }
}

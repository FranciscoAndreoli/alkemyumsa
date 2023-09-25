using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    /// <summary>
    /// Provee métodos para el acceso a datos de Servicios.
    /// </summary>
    public class ServiceRepository : Repository<Servicios>, IServiceRepository
    {
        /// <summary>
        /// Inicializa una nueva instancia de  <see cref="ServiceRepository"/>.
        /// </summary>
        /// <param name="context">Context actúa como una sesión entre la aplicación y la base de datos.</param>
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {


        }

        /// <summary>
        /// Devuelve los Servicios no eliminados.
        /// </summary>
        /// <returns>Una lista de los Servicios activos.</returns>
        public async override Task<List<Servicios>> GetAll()
        {
            try
            {
                var servicios = await _context.Servicio.ToListAsync();
                return servicios.Where(x => x.DeletedAt == null).ToList();
            }
            catch (Exception){throw;}
        }

        /// <summary>
        /// Devuelve un servicio según su ID.
        /// </summary>
        /// <param name="id">El ID del servicio a devolver.</param>
        /// <returns>El servicio. Sino, devuelve null.</returns>
        public async override Task<Servicios?> Get(int id)
        {
            try
            {
                var servicios = await _context.Servicio.SingleOrDefaultAsync(x => x.CodServicio == id);
                if (servicios == null || servicios.DeletedAt != null) { return null; }

                return servicios;
            }
            catch (Exception){throw;}
        }
        /// <summary>
        /// Actualiza la información de un servicio.
        /// </summary>
        /// <param name="updateServicio">El DTO de servicio.</param>
        /// <returns>True si se actualizó correctamente. Sino, false.</returns>
        public async override Task<bool> Update(Servicios updateServicio)
        {
            try
            {
                var servicio = await _context.Servicio.SingleOrDefaultAsync(x => x.CodServicio == updateServicio.CodServicio);
                if (servicio == null || servicio.DeletedAt != null) { return false; }
                servicio.Descr = updateServicio.Descr;
                servicio.Estado = updateServicio.Estado;
                servicio.ValorHora = updateServicio.ValorHora;
                _context.Servicio.Update(servicio);

                return true;
            }
            catch (Exception){throw;}
        }

        /// <summary>
        /// Borrado lógico de servicio.
        /// </summary>
        /// <param name="id">ID del servicio a eliminar.</param>
        /// <returns>True si el servicio fue encontrado. Sino, false.</returns>
        public async override Task<bool> Delete(int id)
        {
            try
            {
                var servicio = await _context.Servicio.Where(x => x.CodServicio == id).SingleOrDefaultAsync();
                if (servicio != null && servicio.DeletedAt == null)
                {
                    //_context.servicio.Remove(user); // borrado físico
                    servicio.DeletedAt = DateTime.UtcNow; // borrado lógico

                    return true;
                }
                return false;
            }
            catch(Exception){ throw; }       
        }

        /// <summary>
        /// Chequea si el servicio ingresado ya existe.
        /// </summary>
        /// <param name="descrServicio">El servicio a chequear.</param>
        /// <returns>True si el servicio ya existe. Sino, false.</returns>
        public async override Task<bool> Check(string descrServicio)
        {
            try
            {
                return await _context.Servicio.AnyAsync(x => x.Descr.ToLower() == descrServicio.ToLower());
            }
            catch (Exception){throw;}    
        }

        /// <summary>
        /// Devuelve listado de servicios activos
        /// </summary>
        /// <returns>Un listado. Sino, un listado vacío.</returns>
        public async Task<List<Servicios>> GetActiveServices()
        {
            try
            {
                return await _context.Servicio.Where(x => x.Estado == true && x.DeletedAt == null).ToListAsync();
            }
            catch (Exception) {throw;}
        }
    }
}

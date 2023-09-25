using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    /// <summary>
    /// Provee métodos para el acceso a datos de proyectos.
    /// </summary>
    public class ProjectRepository : Repository<Proyectos>, IProjectRepository
    {
        /// <summary>
        /// Inicializa una nueva instancia de  <see cref="ProjectRepository"/>.
        /// </summary>
        /// <param name="context">Context actúa como una sesión entre la aplicación y la base de datos.</param>
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {


        }

        /// <summary>
        /// Devuelve los proyectos no eliminados.
        /// </summary>
        /// <returns>Una lista de los proyectos activos.</returns>
        public async override Task<List<Proyectos>> GetAll()
        {
            try
            {
                var proyectos = await _context.Proyecto.ToListAsync();
                return proyectos.Where(x => x.DeletedAt == null).ToList();
            }
            catch (Exception) { throw; }      
        }

        /// <summary>
        /// Devuelve un proyecto según su ID.
        /// </summary>
        /// <param name="id">El ID del proyecto a devolver.</param>
        /// <returns>El proyecto. Sino, devuelve null.</returns>
        public async override Task<Proyectos?> Get(int id)
        {
            try
            {
                var proyectos = await _context.Proyecto.SingleOrDefaultAsync(x => x.CodProyecto == id);
                if (proyectos == null || proyectos.DeletedAt != null) { return null; }

                return proyectos;
            }
            catch (Exception) { throw; }
               
        }

        /// <summary>
        /// Devuelve listado de proyectos, filtrados por su estado: Pendiente, Confirmado, Terminado.
        /// </summary>
        /// <param name="estado">El estado del proyecto a devolver.</param>
        /// <returns>El proyecto. Sino, devuelve null.</returns>
        public async Task<List<Proyectos>> GetProjects(string estado)
        {
            try
            {
                var proyectos = await _context.Proyecto.Where
                                                    (x => x.Estado.ToLower() == estado.ToLower() && x.DeletedAt == null)
                                                    .ToListAsync();
                return proyectos;
            }
            catch (Exception) { throw; }
        }


        /// <summary>
        /// Actualiza la información de un proyecto.
        /// </summary>
        /// <param name="updateProyecto">El DTO de proyecto.</param>
        /// <returns>True si se actualizó correctamente. Sino, false.</returns>
        public async override Task<bool> Update(Proyectos updateProyecto)
        {
            try
            {
                var proyecto = await _context.Proyecto.SingleOrDefaultAsync(x => x.CodProyecto == updateProyecto.CodProyecto);
                if (proyecto == null || proyecto.DeletedAt != null) { return false; }
                proyecto.Nombre = updateProyecto.Nombre;
                proyecto.Direccion = updateProyecto.Direccion;
                proyecto.Estado = updateProyecto.Estado;
                _context.Proyecto.Update(proyecto);

                return true;
            }
            catch (Exception){throw; }      
        }

        /// <summary>
        /// Borrado lógico de proyecto.
        /// </summary>
        /// <param name="id">ID del proyecto a eliminar.</param>
        /// <returns>True si el proyecto fue encontrado. Sino, false.</returns>
        public async override Task<bool> Delete(int id)
        {
            try
            {
                var proyecto = await _context.Proyecto.Where(x => x.CodProyecto == id).SingleOrDefaultAsync();
                if (proyecto != null && proyecto.DeletedAt == null)
                {
                    //_context.Proyecto.Remove(user); // borrado físico
                    proyecto.DeletedAt = DateTime.UtcNow; // borrado lógico

                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
            
        }

        /// <summary>
        /// Chequea si el Proyecto ingresado ya existe.
        /// </summary>
        /// <param name="nombreProyecto">El Proyecto a chequear.</param>
        /// <returns>True si el Proyecto ya existe. Sino, false.</returns>
        public async override Task<bool> Check(string nombreProyecto)
        {
            try
            {
                return await _context.Proyecto.AnyAsync(x => x.Nombre.ToLower() == nombreProyecto.ToLower());
            }
            catch (Exception) { throw; }
        }
    }
}

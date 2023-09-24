using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    /// <summary>
    /// Provides data access methods specific to  operations.
    /// </summary>
    public class ProjectRepository : Repository<Proyectos>, IProjectRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {


        }

        /// <summary>
        /// Retrieves all non-deleted users.
        /// </summary>
        /// <returns>A list of all active users.</returns>
        public async override Task<List<Proyectos>> GetAll()
        {
            var proyectos = await _context.Proyecto.ToListAsync();
            return proyectos.Where(x => x.DeletedAt == null).ToList();
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user if found and not deleted; otherwise, null.</returns>
        public async override Task<Proyectos?> Get(int id)
        {
            var proyectos = await _context.Proyecto.SingleOrDefaultAsync(x => x.CodProyecto == id); 
            if (proyectos == null || proyectos.DeletedAt != null) { return null; }

            return proyectos;
        }
        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="updateProyecto">The user object containing updated details.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public async override Task<bool> Update(Proyectos updateProyecto)
        {
            var proyecto = await _context.Proyecto.SingleOrDefaultAsync(x => x.CodProyecto == updateProyecto.CodProyecto); //Trae el usuario que coincida con el ID.

            if (proyecto == null || proyecto.DeletedAt != null) { return false; }

            proyecto.Nombre = updateProyecto.Nombre;
            proyecto.Direccion = updateProyecto.Direccion;
            proyecto.Estado = updateProyecto.Estado;
        

            _context.Proyecto.Update(proyecto);

            return true;
        }

        /// <summary>
        /// Marks a user as deleted without physically removing them from the database.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the user was found and marked as deleted; otherwise, false.</returns>
        public async override Task<bool> Delete(int id)
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

        /// <summary>
        /// Checks if a user with the provided email already exists.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if a user with the email exists; otherwise, false.</returns>
        public async Task<bool> Check(string nombreProyecto)
        {
            return await _context.Proyecto.AnyAsync(x => x.Nombre == nombreProyecto);
        }
    }
}

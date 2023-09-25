using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    /// <summary>
    /// Provee métodos para el acceso a datos de usuarios.
    /// </summary>
    public class UserRepository : Repository<Usuarios>, IUserRepository
    {
        /// <summary>
        /// Inicializa una nueva instancia de  <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="context">Context actúa como una sesión entre la aplicación y la base de datos.</param>
        public UserRepository(ApplicationDbContext context) : base(context) {

        }

        /// <summary>
        /// Autentica un usuario en base a las credenciales.
        /// </summary>
        /// <param name="dto">El DTO que contiene información de las credenciales de usuario.</param>
        /// <returns>Devuelve el usuario autenticado. Sino, null.</returns>
        public async Task<Usuarios?> AuthenticateCredentials(AuthenticateDto dto)
        {
            try
            {
                return await _context.Usuario.SingleOrDefaultAsync(x => x.Email == dto.Email && x.Contrasena == PasswordHashHelper.EncryptPassword(dto.Contrasena, dto.Email));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Devuelve los usuarios no eliminados.
        /// </summary>
        /// <returns>Una lista de los usuarios activos.</returns>
        public async override Task<List<Usuarios>> GetAll()
        {
            try
            {
                var users = await _context.Usuario.ToListAsync();

                return users.Where(x => x.DeletedAt == null).ToList();
            }catch (Exception ex) { throw; }
        }
        
        /// <summary>
        /// Devuelve un usuario según su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a devolver.</param>
        /// <returns>El usuario. Sino, devuelve null.</returns>
        public async override Task<Usuarios?> Get(int id)
        {
            try
            {
                var user = await _context.Usuario.SingleOrDefaultAsync(x => x.Id == id);
                if (user == null || user.DeletedAt != null) { return null; }

                return user;
            }catch (Exception ex) { throw; }
        }
        /// <summary>
        /// Actualiza la información de un usuario.
        /// </summary>
        /// <param name="updateUser">El DTO de usuario.</param>
        /// <returns>True si se actualizó correctamente. Sino, false.</returns>
        public async override Task<bool> Update(Usuarios updateUser)
        {
            try
            {
                var user = await _context.Usuario.SingleOrDefaultAsync(x => x.Id == updateUser.Id);
                if (user == null || user.DeletedAt != null) { return false; }
                user.Nombre = updateUser.Nombre;
                user.Dni = updateUser.Dni;
                user.Email = updateUser.Email;
                user.Rol = updateUser.Rol;
                user.Contrasena = updateUser.Contrasena;
                _context.Usuario.Update(user);

                return true;
            }catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Borrado lógico de usuario.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>True si el usuario fue encontrado. Sino, false.</returns>
        public async override Task<bool> Delete(int id)
        {
            try
            {
                var user = await _context.Usuario.Where(x => x.Id == id).SingleOrDefaultAsync();
                if (user != null && user.DeletedAt == null)
                {
                    //_context.Usuario.Remove(user); // borrado físico
                    user.DeletedAt = DateTime.UtcNow; // borrado lógico

                    return true;
                }
                return false;
            }catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Chequea si el mail ingresado ya existe.
        /// </summary>
        /// <param name="email">El mail a chequear.</param>
        /// <returns>True si el mail ya existe.Sino, false.</returns>
        public async Task<bool> Check(string email)
        {
            try
            {
                return await _context.Usuario.AnyAsync(x => x.Email == email);
            }catch (Exception){throw;}    
        }
    }
}

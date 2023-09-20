using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    /// <summary>
    /// Provides data access methods specific to user operations.
    /// </summary>
    public class UserRepository : Repository<Usuarios>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public UserRepository(ApplicationDbContext context) : base(context) {


        }

        /// <summary>
        /// Authenticates a user based on provided credentials.
        /// </summary>
        /// <param name="dto">The data transfer object containing user credentials for authentication.</param>
        /// <returns>The authenticated user if credentials are correct; otherwise, null.</returns>
        public async Task<Usuarios?> AuthenticateCredentials(AuthenticateDto dto)
        {
            return await _context.Usuario.SingleOrDefaultAsync(x => x.Email == dto.Email && x.Contrasena == PasswordHashHelper.EncryptPassword(dto.Contrasena, dto.Email));

        }

        /// <summary>
        /// Retrieves all non-deleted users.
        /// </summary>
        /// <returns>A list of all active users.</returns>
        public async override Task<List<Usuarios>> GetAll()
        {
            var users = await _context.Usuario.ToListAsync();
            return users.Where(x => x.DeletedAt == null).ToList();
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user if found and not deleted; otherwise, null.</returns>
        public async override Task<Usuarios?> Get(int id)
        {
            var user = await _context.Usuario.SingleOrDefaultAsync(x => x.Id == id); //Trae el usuario que coincida con el ID.
            if (user == null || user.DeletedAt != null) { return null; }

            return user;
        }
        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="updateUser">The user object containing updated details.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public async override Task<bool> Update(Usuarios updateUser)
        {
            var user = await _context.Usuario.SingleOrDefaultAsync(x => x.Id == updateUser.Id); //Trae el usuario que coincida con el ID.

            if (user == null) { return false; }

            user.Nombre = updateUser.Nombre;
            user.Apellido = updateUser.Apellido;
            user.Email = updateUser.Email;
            user.Rol = updateUser.Rol;
            user.Contrasena = updateUser.Contrasena;

            _context.Usuario.Update(user);

            return true;
        }

        /// <summary>
        /// Marks a user as deleted without physically removing them from the database.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the user was found and marked as deleted; otherwise, false.</returns>
        public async override Task<bool> Delete(int id)
        {
            var user =  await _context.Usuario.Where(x => x.Id == id).SingleOrDefaultAsync();
            if (user != null) {
                //_context.Usuario.Remove(user); // borrado físico
                user.DeletedAt = DateTime.UtcNow; // borrado lógico

                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a user with the provided email already exists.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if a user with the email exists; otherwise, false.</returns>
        public async Task<bool> CheckUser(string email)
        {
            return await _context.Usuario.AnyAsync(x => x.Email == email);
        }
    }
}

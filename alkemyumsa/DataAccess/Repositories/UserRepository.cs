using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using alkemyumsa.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    public class UserRepository : Repository<Usuarios>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) {


        }

        public async Task<Usuarios?> AuthenticateCredentials(AuthenticateDto dto)
        {
            return await _context.Usuario.SingleOrDefaultAsync(x => x.Email == dto.Email && x.Contrasena == PasswordHashHelper.HashPassword(dto.Contrasena));

        }
        public async override Task<List<Usuarios>> GetAll()
        {
            var users = await _context.Usuario.ToListAsync();
            return users.Where(x => x.DeletedAt == null).ToList();
        }
        
        //Get user by id
        public async override Task<Usuarios?> Get(int id)
        {
            var user = await _context.Usuario.SingleOrDefaultAsync(x => x.Id == id); //Trae el usuario que coincida con el ID.
            if (user == null || user.DeletedAt != null) { return null; }

            return user;
        }
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
    }
}

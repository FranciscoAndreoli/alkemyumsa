using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DTOs;
using alkemyumsa.Entities;
using Microsoft.EntityFrameworkCore;

namespace alkemyumsa.DataAccess.Repositories
{
    public class UserRepository : Repository<Usuarios>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) {


        }
       // public GetAll<Usuarios>(){}

        public async Task<Usuarios?> authenticateCredentials(AuthenticateDto dto)
        {
            return await _context.Usuario.SingleOrDefaultAsync(x=> x.Email == dto.Email && x.Contrasena == dto.Contrasena);

        }
    }
}

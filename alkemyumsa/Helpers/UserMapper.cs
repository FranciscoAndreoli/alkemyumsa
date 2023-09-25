using alkemyumsa.DTOs;
using alkemyumsa.Entities;

namespace alkemyumsa.Helpers
{
    public static class UserMapper
    {
        public static UsuarioResponseDto MapToDto(Usuarios user)
        {
            return new UsuarioResponseDto
            {
                Nombre = user.Nombre,
                Dni = user.Dni,
                Email = user.Email,
                Rol = user.Rol
            };
        }
    }
}
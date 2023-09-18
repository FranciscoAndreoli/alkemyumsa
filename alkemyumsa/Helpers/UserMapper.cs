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
                Apellido = user.Apellido,
                Email = user.Email,
                Contrasena = user.Contrasena
            };
        } 
    }
}
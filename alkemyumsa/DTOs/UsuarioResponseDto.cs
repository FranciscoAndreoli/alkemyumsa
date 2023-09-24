using alkemyumsa.Entities;

namespace alkemyumsa.DTOs
{
    public class UsuarioResponseDto
    {
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }

        public Roles Rol { get; set; }
        public string Contrasena { get; set; }
    }
}

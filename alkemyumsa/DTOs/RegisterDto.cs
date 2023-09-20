
using alkemyumsa.Entities;

namespace alkemyumsa.DTOs
{
    public class RegisterDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Roles Rol { get; set; } = Roles.Consultor;
        public string Contrasena { get; set; }
          
            
    }
}

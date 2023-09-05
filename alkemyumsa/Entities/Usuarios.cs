using System.Reflection.Metadata.Ecma335;

namespace alkemyumsa.Entities
{
    public class Usuarios
    {
        public int CodUsuario { get; set; }
        public string? Nombre { get; set; }
        public int Dni { get; set; }
        public int Tipo { get; set;}
        public string? Contrasena { get; set; }
    }
}

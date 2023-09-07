using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace alkemyumsa.Entities
{
    public class Usuarios
    {
        [Key]
        [Column("cod_usuario")]
        public int Id { get; set; }

        [Required]
        [Column("nombre_usuario", TypeName = "VARCHAR(80)")]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido_usuario", TypeName = "VARCHAR(80)")]
        public string Apellido { get; set; }

        [Required]
        [Column("email_usuario", TypeName = "VARCHAR(80)")]
        public string Email { get; set; }
        //public int Dni { get; set; }
        //public int Tipo { get; set;}

        [Required]
        [Column("contrasena_usuario", TypeName = "VARCHAR(50)")]
        public string Contrasena { get; set; }

        /*[Required]
        [Column("roles", TypeName = "INT")]
        public int Roles { get; set; }*/
    }
}

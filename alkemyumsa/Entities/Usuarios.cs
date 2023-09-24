using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using alkemyumsa.DTOs;
using alkemyumsa.Helpers;

namespace alkemyumsa.Entities
{
    public class Usuarios
    {
        public Usuarios(RegisterDto dto) { 
        
            Nombre = dto.Nombre;
            Dni = dto.Dni;
            Email = dto.Email;  
            Contrasena = PasswordHashHelper.EncryptPassword(dto.Contrasena, dto.Email);
            Rol = dto.Rol;
        }

        public Usuarios(RegisterDto dto, int id ) // constructor de sobrecarga
        {
            Id = id;
            Nombre = dto.Nombre;
            Dni = dto.Dni;
            Email = dto.Email;
            Contrasena = PasswordHashHelper.EncryptPassword(dto.Contrasena, dto.Email);
            Rol = dto.Rol;
        }

        public Usuarios() { }

        [Key]
        [Column("cod_usuario")]
        public int Id { get; set; }

        [Required]
        [Column("nombre_usuario", TypeName = "VARCHAR(80)")]
        public string Nombre { get; set; }

        [Required]
        [Column("dni", TypeName = "INT")]
        public int Dni { get; set; }

        [Required]
        [Column("email_usuario", TypeName = "VARCHAR(80)")]
        public string Email { get; set; }

        [Required]
        [Column("contrasena_usuario", TypeName = "VARCHAR(250)")]
        public string Contrasena { get; set; }

        [Required]
        [Column("rol")]
        public Roles Rol { get; set; }

        [Column("deleted_at", TypeName = "DATETIME")]
        public DateTime? DeletedAt { get; set; }

    }
}

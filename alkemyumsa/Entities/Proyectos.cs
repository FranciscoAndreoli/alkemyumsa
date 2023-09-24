using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using alkemyumsa.DTOs;
using alkemyumsa.Helpers;
using System.Net;

namespace alkemyumsa.Entities
{
    public class Proyectos
    {
        public Proyectos()
        {

        }
        public Proyectos(ProyectosDto dto)
        {
            Nombre = dto.Nombre;
            Direccion = dto.Direccion;
            Estado = dto.Estado;
        }
        public Proyectos(ProyectosDto dto, int id) // constructor de sobrecarga
        {
            CodProyecto = id;
            Nombre = dto.Nombre;
            Direccion = dto.Direccion;
            Estado = dto.Estado;
        }

        [Key]
        [Column("cod_proyecto")]
        public int CodProyecto { get; set; }

        [Required]
        [Column("nombre_proyecto", TypeName = "VARCHAR(200)")]
        public string Nombre { get; set; }

        [Required]
        [Column("direccion", TypeName = "VARCHAR(300)")]
        public string Direccion { get; set; }

        /// <summary>
        ///Estados: 1 – Pendiente, 2 – Confirmado, 3 – Terminado
        /// </summary>
        [Required]
        [Column("estado", TypeName = "VARCHAR(20)")]
        public string Estado { get; set; }

        /// <summary>
        ///Tabla DeletedAt se utiliza para el borrado lógico
        /// </summary>

        [Column("deleted_at", TypeName = "DATETIME")]
        public DateTime? DeletedAt { get; set; }
    }
}

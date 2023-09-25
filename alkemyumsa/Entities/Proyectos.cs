using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using alkemyumsa.DTOs;
using alkemyumsa.Helpers;
using System.Net;

namespace alkemyumsa.Entities
{
    /// <summary>
    /// Representa un proyecto dentro del sistema.
    /// </summary>
    public class Proyectos
    {
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Proyectos()
        {

        }

        /// <summary>
        /// Crea una nueva instancia basada en un objeto de transferencia de datos (DTO).
        /// </summary>
        /// <param name="dto">El objeto DTO que representa un proyecto.</param>
        public Proyectos(ProyectosDto dto)
        {
            Nombre = dto.Nombre;
            Direccion = dto.Direccion;
            Estado = dto.Estado;
        }

        /// <summary>
        /// Crea una nueva instancia basada en un objeto de transferencia de datos (DTO) y un identificador específico.
        /// </summary>
        /// <param name="dto">El objeto DTO que representa un proyecto.</param>
        /// <param name="id">El identificador del proyecto.</param>
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
        /// Estado del proyecto. Posibles estados: 1 – Pendiente, 2 – Confirmado, 3 – Terminado.
        /// </summary>
        [Required]
        [Column("estado", TypeName = "VARCHAR(20)")]
        public string Estado { get; set; }

        /// <summary>
        /// Fecha en la que el proyecto fue marcado como eliminado. Utilizado para el borrado lógico.
        /// </summary>
        [Column("deleted_at", TypeName = "DATETIME")]
        public DateTime? DeletedAt { get; set; }
    }
}

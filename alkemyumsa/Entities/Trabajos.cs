using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace alkemyumsa.Entities
{
    public class Trabajos
    {
        public Trabajos()
        {

        }

        [Key]
        [Column("cod_trabajo")]
        public int CodTrabajo { get; set; }

        [Required]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column("cant_horas")]
        public int CantHoras { get; set; }

        [Required]
        [Column("valor_hora")]
        public double ValorHora { get; set; }

        [Required]
        [Column("costo")]
        public double Costo { get; set; }


        [Required]
        [Column("cod_proyecto")]
        public int CodProyecto { get; set; }
        [ForeignKey("CodProyecto")]
        public Proyectos? Proyecto { get; set; }

        [Required]
        [Column("cod_servicio")]
        public int CodServicio { get; set;}

        [ForeignKey("CodServicio")]
        public Servicios? Servicio { get; set; }

        [Column("deleted_at", TypeName = "DATETIME")]
        public DateTime? DeletedAt { get; set; }

    }
}


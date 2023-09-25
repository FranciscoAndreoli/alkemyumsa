using alkemyumsa.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace alkemyumsa.Entities
{
    public class Trabajos
    {
        public Trabajos(TrabajosDto dto)
        {
            Fecha = dto.Fecha;
            CantHoras = dto.CantHoras;
            ValorHora = dto.ValorHora;
            Costo = dto.CantHoras * dto.ValorHora;
            CodProyecto = dto.CodProyecto;
            CodServicio = dto.CodServicio;
        }

        public Trabajos(TrabajosDto dto, int id )
        {
            CodTrabajo = id;
            Fecha = dto.Fecha;
            CantHoras = dto.CantHoras;
            ValorHora = dto.ValorHora;
            Costo = dto.CantHoras * dto.ValorHora;
            CodProyecto = dto.CodProyecto;
            CodServicio = dto.CodServicio;
        }
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


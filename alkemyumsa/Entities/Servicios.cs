using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using alkemyumsa.DTOs;

namespace alkemyumsa.Entities
{
    public class Servicios
    {
        public Servicios(ServiciosDto dtoServicio)
        {
            Descr = dtoServicio.Descr;
            Estado = dtoServicio.Estado;
            ValorHora = dtoServicio.ValorHora;
        }
        public Servicios(ServiciosDto dtoServicio, int id)
        {
            CodServicio = id;
            Descr = dtoServicio.Descr;
            Estado = dtoServicio.Estado;
            ValorHora = dtoServicio.ValorHora;
        }
        public Servicios()
        {

        }

        [Key]
        [Column("cod_servicio")]
        public int CodServicio { get; set; }
       
        [Column("descr_servicio", TypeName = "VARCHAR(300)")]
        public string Descr { get; set; }

        [Column("estado_servicio")]
        public bool Estado { get; set; }

        [Column("valor_hora")]
        public float ValorHora { get; set; }

        [Column("deleted_at", TypeName = "DATETIME")]
        public DateTime? DeletedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace alkemyumsa.DTOs
{
    public class ServiciosDto
    {
        public string Descr { get; set; }
        public bool Estado { get; set; }        
        public float ValorHora { get; set; }
    }
}

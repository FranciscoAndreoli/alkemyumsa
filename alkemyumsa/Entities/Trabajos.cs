using System.Reflection.Metadata.Ecma335;

namespace alkemyumsa.Entities
{
    public class Trabajos
    {
        public int CodTrabajo { get; set; }
        public DateTime Fecha { get; set; }
        public int CodProyecto { get; set; }
        public int CodServicio { get; set;}
        public int CantHoras { get; set;}
        public float ValorHora { get; set;}
        public float Costo { get; set; }


    }
}


﻿using alkemyumsa.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace alkemyumsa.DTOs
{
    public class TrabajosDto
    {
        public DateTime Fecha { get; set; }
        public int CantHoras { get; set; }
        public double ValorHora { get; set; }
        public int CodProyecto { get; set; }
        public int CodServicio { get; set; }
    }
}

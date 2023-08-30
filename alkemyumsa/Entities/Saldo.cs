namespace alkemyumsa.Entities
{
    public class Saldo
    {
        public Saldo()
        {

            id = 0;
            saldo = 0;
            activo = true;

        }
        //propiedades. nos
        //permite almacenar y obtener informacion a traves de get y set.
        public int id { get; set; }
        public double saldo { get; set; }
        public bool activo { get; set; }
    }
}
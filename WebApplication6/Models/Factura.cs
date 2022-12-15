namespace WebApplication6.Models
{
    public class Factura
    {
        public Factura()
        {
            Detalles = new HashSet<Detalle>();
        }

        public int Id { get; set; }
        public string Cliente { get; set; }
        public float Total { get; set; }

        public virtual ICollection<Detalle> Detalles { get; set; }
    }
}

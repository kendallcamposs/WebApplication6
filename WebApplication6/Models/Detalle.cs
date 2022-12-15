namespace WebApplication6.Models
{
    public class Detalle
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int ArticuloId { get; set; }

        public virtual Factura Factura { get; set; }
        public virtual Articulo Articulo { get; set; }
    }
}

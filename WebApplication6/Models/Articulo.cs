namespace WebApplication6.Models
{
    public class Articulo
    {
        public Articulo()
        {
            Categorias = new HashSet<Categoria>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public float Precio { get; set; }
        public int CategoriaId { get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }
    }
}

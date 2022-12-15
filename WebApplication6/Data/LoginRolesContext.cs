using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class LoginRolesContext : DbContext
    {
        public LoginRolesContext(DbContextOptions<LoginRolesContext> options) : base(options)
        {

        }

        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Detalle> Detalles { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}

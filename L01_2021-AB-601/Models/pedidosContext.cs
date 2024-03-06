using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2021_AB_601.Models
{
    public class pedidosContext : DbContext
    {
        public pedidosContext(DbContextOptions<clientesContext> options) : base(options)
        {

        }
        public DbSet<pedidos> pedidos { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2021_AB_601.Models
{
    public class clientesContext : DbContext
    {
        public clientesContext(DbContextOptions<clientesContext> options) : base(options)
        {

        }
        public DbSet<clientes> clientes { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2021_AB_601.Models
{
    public class platosContext : DbContext
    {
        public platosContext(DbContextOptions<platosContext> options) : base(options)
        {

        }

        public DbSet<platos> platos { get; set; }
    }
}

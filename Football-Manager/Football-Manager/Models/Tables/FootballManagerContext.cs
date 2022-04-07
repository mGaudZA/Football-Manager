using Microsoft.EntityFrameworkCore;

namespace Football_Manager.Models.Tables
{
    public class FootballManagerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public FootballManagerContext(DbContextOptions context) : base(context)
        {

        }
    }
}

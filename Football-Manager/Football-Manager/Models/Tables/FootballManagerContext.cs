using Microsoft.EntityFrameworkCore;

namespace Football_Manager.Models.Tables
{
    public class FootballManagerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }


        public FootballManagerContext(DbContextOptions context) : base(context)
        {

        }
    }
}

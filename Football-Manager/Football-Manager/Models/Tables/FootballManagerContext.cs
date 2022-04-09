using Microsoft.EntityFrameworkCore;

namespace Football_Manager.Models.Tables
{
    public class FootballManagerContext : DbContext
    {
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Stadium> Stadiums { get; set; }
        public FootballManagerContext(DbContextOptions<FootballManagerContext> options) : base(options) { }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Football_Manager.Models.Tables
{
    public class FootballManagerContext : DbContext
    {
        public FootballManagerContext(DbContextOptions context) : base(context)
        {

        }
    }
}

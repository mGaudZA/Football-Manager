using Football_Manager.Enums;
using Football_Manager.Models.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager_Tests.TestHelpers
{
    public static class PlayerHelper
    {
        public static Player GetMockPlayer()
        {
            return new Player()
            {
                Name = "test",
                Surname = "player",
                DateOfBirth = DateTime.Now,
                Weight = 75,
                Height = 1.8,
                Position = Positions.Defender,
                NumberOfGoalsScored = 2,
                NumberOfRedCards = 2,
                NumberOfYellowCards = 2,
            };
        }

        public static async Task<FootballManagerContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FootballManagerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new FootballManagerContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Players.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Players.Add(GetMockPlayer());
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

    }
}

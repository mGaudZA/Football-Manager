using Football_Manager.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager_Tests.TestHelpers
{
    internal static class TeamHelper
    {
        public static Team GetMockTeam()
        {
            return new Team()
            {
                Name = "Test Team",
                PassPercentage = 75.5,
                PossessionPercentage = 60.85,
                Wins = 42,
                Losses = 40
            };
        }
    }
}

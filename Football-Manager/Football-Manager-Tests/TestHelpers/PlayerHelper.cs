using Football_Manager.Enums;
using Football_Manager.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Moq;
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
    }
}

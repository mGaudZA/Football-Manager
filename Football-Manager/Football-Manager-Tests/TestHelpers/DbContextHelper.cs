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
    internal static class DbContextHelper
    {
        public static FootballManagerContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<FootballManagerContext>()
            .UseInMemoryDatabase(databaseName: "footballManager")
            .Options;

            return new FootballManagerContext(options);

        }
    }
}

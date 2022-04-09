using Football_Manager.Interfaces;
using Football_Manager.Models.Tables;
using Football_Manager.Providers;
using Football_Manager_Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace Football_Manager_Tests
{
    public class StadiumProviderTests
    {
        private IStadiumProvider _StadiumProvider;
        [Fact]
        public async Task TestAddStadium()
        {
            var dbContext = DbContextHelper.GetDbContext();

            var StadiumProvider = new StadiumProvider(dbContext);

            var Stadium = await StadiumProvider.CreateStadium(StadiumHelper.GetMockStadium());

            Assert.NotNull(Stadium);
        }

        [Fact]
        public async Task TestGetStadium()
        {
            var dbContext = DbContextHelper.GetDbContext();

            dbContext.Stadiums.Add(StadiumHelper.GetMockStadium());

            var StadiumProvider = new StadiumProvider(dbContext);

            var Stadium = await StadiumProvider.GetStadium(1);

            Assert.NotNull(Stadium);
        }

        [Fact]
        public void TestGetAllStadium()
        {
            var dbContext = DbContextHelper.GetDbContext();

            dbContext.Stadiums.Add(StadiumHelper.GetMockStadium());
            dbContext.Stadiums.Add(StadiumHelper.GetMockStadium());
            dbContext.SaveChanges();

            var StadiumProvider = new StadiumProvider(dbContext);

            var Stadiums = StadiumProvider.GetAllStadiums();

            Assert.Equal(Stadiums.Count,2);
        }
    }
}
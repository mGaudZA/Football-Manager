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
    public class PlayerProviderTests
    {
        private IPlayerProvider _PlayerProvider;
        [Fact]
        public async Task TestCreatePlayer()
        {
            var data = new List<Player>
            {
                PlayerHelper.GetMockPlayer()
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Player>>();
            mockSet.As<IQueryable<Player>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Player>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Player>>().Setup(m => m.ElementType).Returns(data.ElementType);

            mockSet.As<IQueryable<Player>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<FootballManagerContext>();
            mockContext.Setup(m => m.Players).Returns(mockSet.Object);

            var service = new Mock<PlayerProvider>(mockContext.Object);

            //var player = await service.Object.AddPlayer(PlayerHelper.GetMockPlayer());
            var player =  service.Object.GetAllPlayers();

            Assert.True(player.Count() > 0);



            //var dbContext = await PlayerHelper.GetDatabaseContext();
            //var playerProvider = new PlayerProvider(dbContext);
            ////Act
            //List<Player> players = playerProvider.GetAllPlayers();
            ////Assert
            //Assert.NotNull(players.Count > 0);
        }
    }
}
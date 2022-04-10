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
        public async Task TestAddPlayer()
        {
            var dbContext = DbContextHelper.GetDbContext();

            var playerProvider = new PlayerProvider(dbContext);

            var player = await playerProvider.AddPlayer(PlayerHelper.GetMockPlayer());

            Assert.NotNull(player);
        }

        [Fact]
        public async Task TestGetPlayer()
        {
            var dbContext = DbContextHelper.GetDbContext();

            dbContext.Players.Add(PlayerHelper.GetMockPlayer());

            var playerProvider = new PlayerProvider(dbContext);

            var player = await playerProvider.GetPlayer(1);

            Assert.NotNull(player);
        }

        [Fact]
        public void TestGetAllPlayer()
        {
            var dbContext = DbContextHelper.GetDbContext();

            dbContext.Players.Add(PlayerHelper.GetMockPlayer());
            dbContext.Players.Add(PlayerHelper.GetMockPlayer());
            dbContext.SaveChanges();

            var playerProvider = new PlayerProvider(dbContext);

            var players = playerProvider.GetAllPlayers();

            Assert.True(players.Count > 0);
        }

        [Fact]
        public async Task TestAddPlayerFailWithInvalidTeam()
        {
            var dbContext = DbContextHelper.GetDbContext();
            var newPlayer = PlayerHelper.GetMockPlayer();
            newPlayer.TeamId = int.MaxValue;
            dbContext.Players.Add(newPlayer);

            var playerProvider = new PlayerProvider(dbContext);

            var player = await playerProvider.AddPlayer(newPlayer);

            Assert.Null(player);
        }
    }
}
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
using Football_Manager.Models.Request;

namespace Football_Manager_Tests
{
    public class TeamProviderTests
    {
        private ITeamProvider _TeamProvider;
        [Fact]
        public async Task TestAddTeam()
        {
            var dbContext = DbContextHelper.GetDbContext();

            var TeamProvider = new TeamProvider(dbContext);

            var Team = await TeamProvider.CreateTeam(TeamHelper.GetMockTeam());

            Assert.NotNull(Team);
        }

        [Fact]
        public async Task TestGetTeam()
        {
            var dbContext = DbContextHelper.GetDbContext();

            dbContext.Teams.Add(TeamHelper.GetMockTeam());

            var TeamProvider = new TeamProvider(dbContext);

            var Team = await TeamProvider.GetTeam(1);

            Assert.NotNull(Team);
        }

        [Fact]
        public void TestGetAllTeam()
        {
            var dbContext = DbContextHelper.GetDbContext();

            dbContext.Teams.Add(TeamHelper.GetMockTeam());
            dbContext.Teams.Add(TeamHelper.GetMockTeam());
            dbContext.SaveChanges();

            var TeamProvider = new TeamProvider(dbContext);

            var Teams = TeamProvider.GetAllTeams();

            Assert.True(Teams.Count > 0);
        }

        [Fact]
        public async Task TestAddTeamFailWithInvalidStadium()
        {
            var dbContext = DbContextHelper.GetDbContext();
            var newTeam = TeamHelper.GetMockTeam();
            newTeam.StadiumId = 1;
            dbContext.Teams.Add(newTeam);

            var TeamProvider = new TeamProvider(dbContext);

            var Team = await TeamProvider.CreateTeam(newTeam);

            Assert.Null(Team);
        }

        [Fact]
        public async Task TestChangePlayerTeam()
        {
            var dbContext = DbContextHelper.GetDbContext();

            dbContext.Teams.Add(TeamHelper.GetMockTeam());
            dbContext.Teams.Add(TeamHelper.GetMockTeam());
            dbContext.Teams.Add(TeamHelper.GetMockTeam());

            dbContext.SaveChanges();

            var player = PlayerHelper.GetMockPlayer();
            player.TeamId = dbContext.Teams.FirstOrDefault().Id;

            dbContext.Players.Add(player);

            var playerProvider = new PlayerProvider(dbContext);
            var newPlayer = await playerProvider.AddPlayer(player);

            Assert.Equal(newPlayer.TeamId, dbContext.Teams.FirstOrDefault().Id);

            var teamProvider = new TeamProvider(dbContext);

            teamProvider.LinkPlayerToTeam(new LinkPlayerToTeamRequest() { PlayerId = newPlayer.PlayerId, TeamId = dbContext.Teams.LastOrDefault().Id });

            newPlayer = await playerProvider.GetPlayer(newPlayer.PlayerId);

            Assert.Equal(newPlayer.TeamId, dbContext.Teams.LastOrDefault().Id);


        }
    }
}
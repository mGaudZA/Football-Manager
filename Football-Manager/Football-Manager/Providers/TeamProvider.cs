using Football_Manager.Interfaces;
using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;
using System.Linq;
namespace Football_Manager.Providers
{
    public class TeamProvider : ITeamProvider
    {
        public FootballManagerContext _footballManagerContext;
        public TeamProvider(FootballManagerContext footballManagerContext)
        {
            _footballManagerContext = footballManagerContext;
        }


        public async Task<Team> GetTeam(int teamId)
        {
            return await _footballManagerContext.Teams.FindAsync(teamId);
        }

        public List<Team> GetAllTeams()
        {
            return _footballManagerContext.Teams.ToList();
        }

        public async Task<bool> CreateTeam(CreateOrUpdateTableRequest newTeam)
        {
            var team = new Team() {
                Name = newTeam.Name,
                PassPercentage = newTeam.PassPercentage,
                PossessionPercentage = newTeam.PossessionPercentage,
                Losses = newTeam.Losses,
                Wins = newTeam.Wins,


            };
            var teamConfirmation = await _footballManagerContext.Teams.AddAsync(team);
            await _footballManagerContext.SaveChangesAsync();

            return _footballManagerContext.Teams.Any(x => x.Id == teamConfirmation.Entity.Id);
        }

        public async Task<bool> DeleteTeam(int teamId)
        {
            var team = await _footballManagerContext.Teams.FindAsync(teamId);

            if (team != null)
            {
                _footballManagerContext.Teams.Remove(team);
                await _footballManagerContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LinkPlayerToTeam(LinkPlayerToTeamRequest request)
        {
            var player = await _footballManagerContext.Players.FindAsync(request.PlayerId);
            if(player == null)
            {
                return false;
            }
            var team = await _footballManagerContext.Teams.FindAsync(request.TeamId);
            if (team == null)
            {
                return false;
            }
            player.TeamId = team.Id;

            await _footballManagerContext.SaveChangesAsync();
            return true;
        }

    }
}

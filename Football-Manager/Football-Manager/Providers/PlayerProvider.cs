using Football_Manager.Interfaces;
using Football_Manager.Models.Tables;

namespace Football_Manager.Providers
{
    public class PlayerProvider : IPlayerProvider
    {
        public FootballManagerContext _footballManagerContext;
        public PlayerProvider(FootballManagerContext footballManagerContext)
        {
            _footballManagerContext = footballManagerContext;
        }

        public async Task<Player> GetPlayer(int playerId)
        {
            return await _footballManagerContext.Players.FindAsync(playerId);
        }

        public List<Player> GetAllPlayers()
        {
            return _footballManagerContext.Players.ToList();
        }

        public async Task<Player> AddPlayer(Player newPlayer)
        {
            var player = await _footballManagerContext.Players.AddAsync(newPlayer);
            await _footballManagerContext.SaveChangesAsync();

            return await _footballManagerContext.Players.FindAsync(player.Entity.PlayerId);
        }

        public async Task<bool> DeletePlayer(int playerId)
        {
            var player = await _footballManagerContext.Players.FindAsync(playerId);

            if(player != null)
            {
                _footballManagerContext.Players.Remove(player);
                await _footballManagerContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Player>> GetAllPlayersByTeamId(int teamId)
        {
            var team = await _footballManagerContext.Teams.FindAsync(teamId);

            if(team == null)
            {
                return null;
            }
            return _footballManagerContext.Players.Where(b => b.TeamId == teamId).ToList();
        }
    }
}

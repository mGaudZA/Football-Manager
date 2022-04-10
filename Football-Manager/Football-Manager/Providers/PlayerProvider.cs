using Football_Manager.Interfaces;
using Football_Manager.Models;
using Football_Manager.Models.Tables;

namespace Football_Manager.Providers
{
    public class PlayerProvider : IPlayerProvider
    {
        private FootballManagerContext _footballManagerContext;
        private IPortraitProvider _portraitProvider;
        public PlayerProvider(FootballManagerContext footballManagerContext, IPortraitProvider portraitProvider)
        {
            _footballManagerContext = footballManagerContext;
            _portraitProvider = portraitProvider;
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
            newPlayer.PortraitKey = String.Empty;
            if(newPlayer.TeamId != 0 && (await _footballManagerContext.Teams.FindAsync(newPlayer.TeamId)) == null)
            {
                return null;
            }

            var player = await _footballManagerContext.Players.AddAsync(newPlayer);
            await _footballManagerContext.SaveChangesAsync();

            return player.Entity;
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

        public async Task<Player> UpdatePlayer(Player updatedPlayer)
        {
            var currentPlayer = await _footballManagerContext.Players.FindAsync(updatedPlayer.PlayerId);

            if(currentPlayer == null)
            {
                return null;
            }
            currentPlayer.Name = updatedPlayer.Name;
            currentPlayer.Surname = updatedPlayer.Surname;
            currentPlayer.NumberOfRedCards = updatedPlayer.NumberOfRedCards;
            currentPlayer.NumberOfYellowCards = updatedPlayer.NumberOfYellowCards;
            currentPlayer.NumberOfGoalsScored = currentPlayer.NumberOfGoalsScored;
            currentPlayer.DateOfBirth = currentPlayer.DateOfBirth;
            currentPlayer.Position = updatedPlayer.Position;
            currentPlayer.Height = updatedPlayer.Height;
            currentPlayer.Weight = updatedPlayer.Weight;

            await _footballManagerContext.SaveChangesAsync();

            return currentPlayer;
        }

        public async Task<List<PlayerResponse>> GetAllPlayersWithPortraits()
        {
            var players = GetAllPlayers();

            List<Tuple<Player, Task<string>>> playerPortraitTasks = players.ToList().Select(x => { return new Tuple<Player, Task<string>>(x,_portraitProvider.GetPlayerPortrait( x.PlayerId)); }).ToList();
            await Task.WhenAll(playerPortraitTasks.Select(x => x.Item2));

            return playerPortraitTasks.Select(x => { return new PlayerResponse() { player = x.Item1, PlayerPortrait = x.Item2.Result }; }).ToList();
        }
    }
}

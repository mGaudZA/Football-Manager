using Football_Manager.Models;
using Football_Manager.Models.Tables;

namespace Football_Manager.Interfaces
{
    public interface IPlayerProvider
    {
        Task<Player> GetPlayer(int playerId);
        List<Player> GetAllPlayers();
        Task<Player> AddPlayer(Player newPlayer);
        Task<bool> DeletePlayer(int playerId);
        Task<List<Player>> GetAllPlayersByTeamId(int teamId);
        Task<Player> UpdatePlayer(Player updatedPlayer);
        Task<List<PlayerResponse>> GetAllPlayersWithPortraits();
    }
}

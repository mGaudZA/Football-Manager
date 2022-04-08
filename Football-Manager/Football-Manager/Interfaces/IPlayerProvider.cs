using Football_Manager.Models.Tables;

namespace Football_Manager.Interfaces
{
    public interface IPlayerProvider
    {
        Task<Player> GetPlayer(int playerId);
        List<Player> GetAllPlayers();
        Task<bool> AddPlayer(Player newPlayer);
        Task<bool> DeletePlayer(int playerId);
        Task<List<Player>> GetAllPlayersByTeamId(int teamId);
    }
}

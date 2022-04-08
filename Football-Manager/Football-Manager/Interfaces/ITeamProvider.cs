using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;

namespace Football_Manager.Interfaces
{
    public interface ITeamProvider
    {
        Task<Team> GetTeam(int teamId);
        Task<Team> CreateTeam(CreateOrUpdateTableRequest newTeam);
        Task<bool> DeleteTeam(int teamId);
        List<Team> GetAllTeams();
        Task<bool> LinkPlayerToTeam(LinkPlayerToTeamRequest request);
    }
}

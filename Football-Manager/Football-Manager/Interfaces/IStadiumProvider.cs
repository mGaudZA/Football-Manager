using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;

namespace Football_Manager.Interfaces
{
    public interface IStadiumProvider
    {
        Task<Stadium> CreateStadium(Stadium newStadium);
        Task<bool> DeleteStadium(int stadiumId);
        List<Stadium> GetAllStadiums();
        Task<Stadium> GetStadium(int stadiumId);
        Task<bool> LinkTeamToStadium(LinkTeamToStadiumRequest request);
    }
}
using Football_Manager.Models.Request;

namespace Football_Manager.Interfaces
{
    public interface IPortraitProvider
    {
        Task<string> AddPortraitToPlayer(AddPortraitToPlayerRequest request);
        Task<string> GetPlayerPortrait(int PlayerID);
    }
}
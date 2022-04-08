using Football_Manager.Interfaces;
using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;
using System.Linq;
namespace Football_Manager.Providers
{
    public class StadiumProvider : IStadiumProvider
    {
        public FootballManagerContext _footballManagerContext;
        public StadiumProvider(FootballManagerContext footballManagerContext)
        {
            _footballManagerContext = footballManagerContext;
        }

        public async Task<Stadium> GetStadium(int stadiumId)
        {
            return await _footballManagerContext.Stadiums.FindAsync(stadiumId);
        }

        public List<Stadium> GetAllStadiums()
        {
            return _footballManagerContext.Stadiums.ToList();
        }

        public async Task<Stadium> CreateStadium(Stadium newStadium)
        {

            var stadiumConfirmation = await _footballManagerContext.Stadiums.AddAsync(newStadium);
            await _footballManagerContext.SaveChangesAsync();

            return await _footballManagerContext.Stadiums.FindAsync(stadiumConfirmation.Entity.StadiumId);
        }

        public async Task<bool> DeleteStadium(int stadiumId)
        {
            var stadium = await _footballManagerContext.Stadiums.FindAsync(stadiumId);

            if (stadium != null)
            {
                _footballManagerContext.Stadiums.Remove(stadium);

                foreach (var player in _footballManagerContext.Teams.Where(x => x.StadiumId == stadiumId))
                {
                    player.StadiumId = 0;
                }
                await _footballManagerContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LinkTeamToStadium(LinkTeamToStadiumRequest request)
        {
            var team = await _footballManagerContext.Teams.FindAsync(request.TeamId);
            if (team == null)
            {
                return false;
            }
            var stadium = await _footballManagerContext.Stadiums.FindAsync(request.StadiumId);
            if (stadium == null)
            {
                return false;
            }
            team.StadiumId = stadium.StadiumId;

            await _footballManagerContext.SaveChangesAsync();
            return true;
        }

    }
}

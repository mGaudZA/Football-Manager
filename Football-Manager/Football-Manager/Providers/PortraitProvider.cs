using Football_Manager.Interfaces;
using Football_Manager.Models;
using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Football_Manager.Providers
{
    public class PortraitProvider : IPortraitProvider
    {
        public FootballManagerContext _footballManagerContext;
        public IDistributedCache _IDistributedCache;
        private readonly IConfiguration _configuration;

        public PortraitProvider(FootballManagerContext footballManagerContext, IDistributedCache _iDistributedCache, IConfiguration configuration)
        {
            _footballManagerContext = footballManagerContext;
            _IDistributedCache = _iDistributedCache;
            _configuration = configuration;
        }

        public async Task<string> AddPortraitToPlayer(AddPortraitToPlayerRequest request)
        {
            var player = await _footballManagerContext.Players.FindAsync(request.PlayerId);

            if (player == null)
            {
                return null;
            }

            string cachedDataString = JsonSerializer.Serialize(request.PortraitBase64String);
            var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

            var portraitKey = $"{_configuration["PortraitPrefix"]}{player.PlayerId}";

            await _IDistributedCache.SetAsync(portraitKey, dataToCache);

            player.PortraitKey = portraitKey;

            await _footballManagerContext.SaveChangesAsync();

            return portraitKey;
        }

        public async Task<string> GetPlayerPortrait(int PlayerID)
        {
            var player = await _footballManagerContext.Players.FindAsync(PlayerID);

            if (player == null)
            {
                return null;
            }

            var portraitByteArray = await _IDistributedCache.GetAsync(player.PortraitKey);

            if(portraitByteArray == null)
            {
                return null;
            }

            var cachedDataString = Encoding.UTF8.GetString(portraitByteArray);
            var portrait = JsonSerializer.Deserialize<string>(cachedDataString);

            return portrait;
        }
    }
}

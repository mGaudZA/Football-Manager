using Football_Manager.Interfaces;
using Football_Manager.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Football_Manager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PlayerController : ControllerBaseOverride
    {
        public IPlayerProvider _playerProvider;
        public PlayerController(IPlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody]Player request)
        {
            try
            {
                var newPlayer = await _playerProvider.AddPlayer(request);

                if (newPlayer != null)
                {
                    return Ok(newPlayer);
                }
                else
                {
                    return BadRequest($"Team with Id {request.TeamId} is not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            try
            {
                var player = await _playerProvider.GetPlayer(playerId);

                if(player != null)
                {
                    return Ok(player);
                }
                else
                {
                     return NotFound($"Player with id {playerId} is not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpGet()]
        public IActionResult GetAllPayers()
        {
            try
            {
                var players = _playerProvider.GetAllPlayers();

                return Ok(players);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            try
            {
                if(await _playerProvider.DeletePlayer(playerId))
                {
                    return Ok($"Player with ID {playerId} was successfully deleted");
                }
                else
                {
                    return NotFound($"Player with ID {playerId} was not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersByTeamId(int teamId)
        {
            
            try
            {
                var players = await _playerProvider.GetAllPlayersByTeamId(teamId);
                if (players != null)
                {
                    return Ok(players);
                }
                else
                {
                    return NotFound($"team with ID {teamId} was not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlayer(Player player)
        {
            try
            {
                var players = await _playerProvider.UpdatePlayer(player);
                if (players != null)
                {
                    return Ok(players);
                }
                else
                {
                    return NotFound($"Player with Id {player.PlayerId} Is not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }
    }
}

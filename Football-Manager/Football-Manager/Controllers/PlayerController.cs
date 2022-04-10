using Football_Manager.Interfaces;
using Football_Manager.Models;
using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Football_Manager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PlayerController : ControllerBaseOverride
    {
        public IPlayerProvider _playerProvider;
        public IPortraitProvider _portraitProvider;
        public PlayerController(IPlayerProvider playerProvider, IPortraitProvider portraitProvider)
        {
            _playerProvider = playerProvider;
            _portraitProvider = portraitProvider;
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

                if (player != null)
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

        [HttpPost]
        public async Task<IActionResult> AddPortraitToPlayer([FromBody] AddPortraitToPlayerRequest request)
        {
            try
            {
                var portrait = await _portraitProvider.AddPortraitToPlayer(request);
                if (portrait != null)
                {
                    return Ok(portrait);
                }
                else
                {
                    return NotFound($"Player with Id {request.PlayerId} Is not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPortraitForPlayer(int playerId)
        {
            try
            {
                var portrait = await _portraitProvider.GetPlayerPortrait(playerId);
                if (portrait != null)
                {
                    return Ok(portrait);
                }
                else
                {
                    return NotFound($"Player with Id playerId Is not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPlayersWithPortraits()
        {
            try
            {
                var portrait = await _playerProvider.GetAllPlayersWithPortraits();
                if (portrait != null)
                {
                    return Ok(portrait);
                }
                else
                {
                    return NotFound($"No players Found");
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

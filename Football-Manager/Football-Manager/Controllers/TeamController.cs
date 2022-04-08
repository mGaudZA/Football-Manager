using Football_Manager.Interfaces;
using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Football_Manager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TeamController : ControllerBaseOverride
    {
        public ITeamProvider _teamProvider;
        public TeamController(ITeamProvider teamProvider)
        {
            _teamProvider = teamProvider;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateOrUpdateTableRequest request)
        {
            try
            {
                var newTeam = await _teamProvider.CreateTeam(request);

                return Ok(newTeam);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetTeam(int teamId)
        {
            try
            {
                var team = await _teamProvider.GetTeam(teamId);

                if (team != null)
                {
                    return Ok(team);
                }
                else
                {
                    return NotFound($"Team with id {teamId} is not found");
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
                var team = _teamProvider.GetAllTeams();

                return Ok(team);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            try
            {
                if (await _teamProvider.DeleteTeam(teamId))
                {
                    return Ok($"Team with ID {teamId} was successfully deleted");
                }
                else
                {
                    return NotFound($"Team with ID {teamId} was not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LinkPlayerToTeam([FromBody]LinkPlayerToTeamRequest request)
        {
            try
            {
                if (await _teamProvider.LinkPlayerToTeam(request))
                {
                    return Ok($"Player with ID {request.PlayerId} was successfully linked to team with ID {request.TeamId}");
                }
                else
                {
                    return NotFound($"Team with ID  was not found");
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

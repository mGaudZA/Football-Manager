using Football_Manager.Interfaces;
using Football_Manager.Models.Request;
using Football_Manager.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Football_Manager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StadiumController : ControllerBaseOverride
    {
        public IStadiumProvider _StadiumProvider;
        public StadiumController(IStadiumProvider StadiumProvider)
        {
            _StadiumProvider = StadiumProvider;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStadium([FromBody] Stadium request)
        {
            try
            {
                var newStadium = await _StadiumProvider.CreateStadium(request);
                if(newStadium != null)
                {
                    return Ok(newStadium);
                }
                else
                {
                    throw new Exception("failed to add Stadium");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetStadium(int StadiumId)
        {
            try
            {
                var Stadium = await _StadiumProvider.GetStadium(StadiumId);

                if (Stadium != null)
                {
                    return Ok(Stadium);
                }
                else
                {
                    return NotFound($"Stadium with id {StadiumId} is not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpGet()]
        public IActionResult GetAllStadiums()
        {
            try
            {
                var Stadium = _StadiumProvider.GetAllStadiums();

                return Ok(Stadium);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteStadium(int StadiumId)
        {
            try
            {
                if (await _StadiumProvider.DeleteStadium(StadiumId))
                {
                    return Ok($"Stadium with ID {StadiumId} was successfully deleted");
                }
                else
                {
                    return NotFound($"Stadium with ID {StadiumId} was not found");
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Service unavailable");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LinkTeamToStadium([FromBody]LinkTeamToStadiumRequest request)
        {
            try
            {
                if (await _StadiumProvider.LinkTeamToStadium(request))
                {
                    return Ok($"Team with ID {request.TeamId} was successfully linked to Stadium with ID {request.StadiumId}");
                }
                else
                {
                    return NotFound($"Stadium with ID  was not found");
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

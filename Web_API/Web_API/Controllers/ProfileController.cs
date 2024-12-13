using BusinessAccessLayer;
using Logger;
using Microsoft.AspNetCore.Mvc;
using WindowsFormApp.Logger;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IBAL _bal;
        private ILog _logger = new Log();

        public ProfileController(IBAL bal)
        {
            _bal = bal;
        }

        // GET PROFILE
        [HttpGet("GetProfile")]
        public ActionResult<List<Models.ProfileModel>> GetProfiles()
        {
            try
            {
                var profiles = _bal.GetProfiles();
                if (profiles == null || profiles.Count == 0)
                {
                    return NotFound("No profiles found.");
                }
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage("Error occurred while fetching profiles." + ex, 3);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

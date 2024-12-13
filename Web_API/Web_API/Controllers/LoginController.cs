using BusinessAccessLayer;
using Logger;
using Microsoft.AspNetCore.Mvc;
using Web_API.Models;
using WindowsFormApp.Logger;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IBAL _bal;
        private ILog _logger = new Log();

        public LoginController(IBAL bal)
        {
            _bal = bal;
        }

        // LOGIN
        [HttpPost("LoginUser")]
        public IActionResult Login(LoginModel userModel)
        {
            try
            {
                var user = _bal.Login(userModel.Email, userModel.Password);
                if (user != null)
                {
                    return Ok(user + "\nLogin Successful");
                }
                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                _logger.LoggerMessage($"Error occurred during login attempt for user: { userModel.Email}", 3);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

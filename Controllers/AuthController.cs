using Microsoft.AspNetCore.Mvc;
using SmartInventoryAPI.Helpers;

namespace SmartInventoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenHelper _jwtHelper;

        public AuthController(JwtTokenHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // ------------------ DEMO USERS ------------------
            // In real projects → fetch from DB (EF Core/Identity)
            if (request.Username == "admin" && request.Password == "password")
            {
                // Generate token with role "Admin"
                var token = _jwtHelper.GenerateToken(request.Username, "Admin");
                return Ok(new { Token = token });
            }
            else if (request.Username == "user" && request.Password == "password")
            {
                // Generate token with role "User"
                var token = _jwtHelper.GenerateToken(request.Username, "User");
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}

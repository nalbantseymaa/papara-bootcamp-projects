using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly FakeAuthService _authService;

    public AuthController(FakeAuthService authService)
    {
        _authService = authService;
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Logging
        await HttpContext.LogActionAsync($"Login attempt for username: {request.Username}");

        var user = _authService.Login(request.Username, request.Password);
        if (user == null)
        {
            // Log failed login attempt
            await HttpContext.LogActionAsync($"Invalid login attempt for username: {request.Username}");
            return Unauthorized(new { Message = "Invalid username or password." });
        }

        // Log successful login
        await HttpContext.LogActionAsync($"Login successful for username: {request.Username}");

        return Ok(new { Message = "Login successful!", UserId = user.Id, ApiKey = FakeAuthService.ApiKey });
    }

    [HttpGet("protected")]
    [RequireAuth]
    public async Task<IActionResult> ProtectedEndpoint()
    {
        // Log access to protected endpoint
        await HttpContext.LogActionAsync("Accessed protected endpoint.");

        return Ok(new { Message = "You have accessed the protected endpoint successfully!" });
    }
}

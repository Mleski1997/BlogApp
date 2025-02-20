using BlogApp.DTO;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

                var token = await _authService.Login(login);

                if (token == null)
                    return Unauthorized("Nieprawidłowa nazwa użytkownika lub hasło.");

                return Ok(new { Token = token });
            }
        }
    }

using HelfenNeuGedacht.API.Application.Services;
using HelfenNeuGedacht.API.Application.Services.Auth.AuthService;
using HelfenNeuGedacht.API.Application.Services.Auth.Dto;
using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelfenNeuGedacht.API.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await authService.LoginAsync(model);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await authService.RegisterAsync(model);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
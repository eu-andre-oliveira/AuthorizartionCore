using Application.Models.Authorizations;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthorizationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public IActionResult Login()
        {
            return Ok(_tokenService.GenerateToken(new LoginRequest("kodoroph","126456")));
        }

        [Authorize]
        [HttpPost("Register")]
        [Authorize]
        public IActionResult Register(RegisterUserRequest registerUserRequest)
        {
            return Ok();
        }
    }
}

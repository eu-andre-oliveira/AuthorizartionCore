using Application.Services.Interfaces;
using Data.DataContext;
using Domain.Models.Authorizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ApiContext _context;

        public AuthorizationController(ITokenService tokenService, ApiContext context)
        {
            _tokenService = tokenService;
            _context = context;
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
            return Ok(_context.MyUsers.ToList());
        }
    }
}

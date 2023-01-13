using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<CRUDReturn>> Login (UserDto credentials)
        {
            return Ok(await _authService.Login(credentials));
        }
        [HttpPost]
        public async Task<ActionResult<CRUDReturn>> Register(UserRegisterDto credentials)
        {
            return Ok(await _authService.Register(credentials));
        }
        [HttpPatch]
        public async Task<ActionResult<CRUDReturn>> ChangePassword (string newPassword, User user)
        {
            return Ok(await _authService.ChangePassword(newPassword, user));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]

        public async Task<ActionResult<CRUDReturn>> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CRUDReturn>> GetUser(int id)
        {
            return Ok(await _userService.GetById(id));
        }


        [HttpPatch]
        public async Task<ActionResult<CRUDReturn>> UpdateUser (UserUpdateDto newUser)
        {
            return Ok(await _userService.UpdateUser(newUser));
        }

    }
}

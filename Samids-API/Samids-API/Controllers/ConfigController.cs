using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpPost]
        public async Task<ActionResult<CRUDReturn>> NewConfig(Config config)
        {
            return Ok(await _configService.NewConfig(config));
        }


    }
}


using AspenCapital.Models.War;
using AspenCapital.Services.War;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AspenCapital.Api.War.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifetimeController : ControllerBase
    {
        private readonly IWarProcessor _warProcessor;

        public LifetimeController(IWarProcessor warProcessor)
        {
            _warProcessor = warProcessor;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<GameDetails>), (int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] string name)
        {
            var games = _warProcessor.GetWinsByPlayer(name);
            return Ok(games);
        }
    }
}

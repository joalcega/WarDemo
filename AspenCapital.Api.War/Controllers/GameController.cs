using AspenCapital.Models.War;
using AspenCapital.Models.War.Requests;
using AspenCapital.Services.War;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AspenCapital.Api.War.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IWarProcessor _warProcessor;

        public GameController(IWarProcessor warProcessor)
        {
            _warProcessor = warProcessor;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GameDetails), (int)HttpStatusCode.Created)]
        public IActionResult Create([FromBody] CreateGame request)
        {
            var game = _warProcessor.CreateGame(request.Player1Name, request.Player2Name);
            return Created($"api/game/{game.Id}", game);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(GameDetails), (int)HttpStatusCode.OK)]
        public IActionResult Get(Guid id)
        {
            var game = _warProcessor.GetGameDetails(id);
            return Ok(game);
        }

        [HttpGet("{id:Guid}/{number}")]
        [ProducesResponseType(typeof(Movement), (int)HttpStatusCode.OK)]
        public IActionResult GetMovement(Guid id, int number)
        {
            var movement = _warProcessor.GetMovement(id, number);
            return Ok(movement);
        }
    }
}

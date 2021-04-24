using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizPrototype.Domain.Entities;
using QuizPrototype.WebApi.Commands;
using System.Threading.Tasks;

namespace QuizPrototype.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStartController : ControllerBase
    {
        private readonly IMediator mediator;

        public GameStartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<Game>> Start()
        {
            var gameStart = await mediator.Send(new SendGameStartCommand());
            return Ok(gameStart);
        }
    }
}

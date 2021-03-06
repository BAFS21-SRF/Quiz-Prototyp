using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizPrototype.Domain.Entities;
using QuizPrototype.WebApi.Commands;
using System.Threading.Tasks;

namespace QuizPrototype.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrageController : ControllerBase
    {
        private readonly IMediator mediator;

        public FrageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<Frage>> GetCurrentFrage([FromQuery] string guid)
        {
            var frage = await mediator.Send(new SendFrageCommand { Guid = guid });
            return Ok(frage);
        }
    }
}

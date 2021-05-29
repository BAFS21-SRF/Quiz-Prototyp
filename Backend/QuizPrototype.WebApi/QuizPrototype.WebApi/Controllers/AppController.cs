using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizPrototype.Domain.Entities;
using QuizPrototype.WebApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizPrototype.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class AppController : ControllerBase
    {
        private readonly IMediator mediator;

        public AppController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<Frage>> GetCurrentFrageFromApp([FromQuery] string guid, long frageId, long score)
        {
            var frage = await mediator.Send(new SendNextFrageCommand { Guid = guid, FrageId = frageId, Score = score });
            return Ok(frage);
        }
    }
}

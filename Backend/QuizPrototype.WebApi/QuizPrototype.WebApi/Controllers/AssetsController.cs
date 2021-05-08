using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizPrototype.Domain.Entities;
using QuizPrototype.WebApi.Commands;
using System.IO;
using System.Threading.Tasks;

namespace QuizPrototype.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AssetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{os}/{assetId}")]
        public ActionResult GetFile(string os, string assetId)
        {
            var filePath = Path.Combine(
           Directory.GetCurrentDirectory(), "assets", $"{assetId}-{os}");
            
            return PhysicalFile(filePath, "unknown/unknown");
  
        }
    }
}

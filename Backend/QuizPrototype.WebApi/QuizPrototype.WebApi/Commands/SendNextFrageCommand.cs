using MediatR;
using QuizPrototype.Domain.Entities;

namespace QuizPrototype.WebApi.Commands
{
    public class SendNextFrageCommand : IRequest<Frage>
    {
        public long Score { get; set; }
        public string Guid { get;  set; }
        public long FrageId { get;  set; }
    }
}

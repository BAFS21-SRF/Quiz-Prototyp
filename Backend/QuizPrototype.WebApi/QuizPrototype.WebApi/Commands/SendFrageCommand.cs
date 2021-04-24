using MediatR;
using QuizPrototype.Domain.Entities;

namespace QuizPrototype.WebApi.Commands
{
    public class SendFrageCommand : IRequest<Frage>
    {
        public string Guid { get; set; }
    }
}

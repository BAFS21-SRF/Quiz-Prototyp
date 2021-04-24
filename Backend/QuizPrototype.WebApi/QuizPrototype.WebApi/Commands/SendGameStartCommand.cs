using MediatR;
using QuizPrototype.Domain.Entities;

namespace QuizPrototype.WebApi.Commands
{
    public class SendGameStartCommand : IRequest<Game>
    {
    }
}

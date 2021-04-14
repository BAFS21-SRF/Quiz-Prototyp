using MediatR;
using QuizPrototype.WebApi.Models;

namespace QuizPrototype.WebApi.Commands
{
    public class SendFrageCommand : IRequest<Frage>
    {
    }
}

using MediatR;
using QuizPrototype.Domain.Entities;
using System.Collections.Generic;

namespace QuizPrototype.WebApi.Commands
{
    public class SendGamesCommand : IRequest<List<Game>>
    {
    }
}

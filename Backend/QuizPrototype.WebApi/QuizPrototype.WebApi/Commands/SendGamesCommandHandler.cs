using MediatR;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuizPrototype.WebApi.Commands
{
    public class SendGamesCommandHandler : IRequestHandler<SendGamesCommand, List<Game>>
    {
        private readonly IGameRepository gameRepository;
       public SendGamesCommandHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }
        public Task<List<Game>> Handle(SendGamesCommand request, CancellationToken cancellationToken)
        {
            return gameRepository.GetAll();
        }
    }
}

using MediatR;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QuizPrototype.WebApi.Commands
{
    public class SendGameStartCommandHandler : IRequestHandler<SendGameStartCommand, Game>
    {
        private readonly IGameService gameService;

        public SendGameStartCommandHandler(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public Task<Game> Handle(SendGameStartCommand request, CancellationToken cancellationToken)
        {
            return gameService.StartGame();
        }
    }
}

using MediatR;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QuizPrototype.WebApi.Commands
{
    public class SendFrageCommandHandler : IRequestHandler<SendFrageCommand, Frage>
    {
        private readonly IFrageRepository frageRepository;
        private readonly IGameService gameService;

        public SendFrageCommandHandler(IFrageRepository frageRepository, IGameService gameService)
        {
            this.frageRepository = frageRepository;
            this.gameService = gameService;
        }

        public Task<Frage> Handle(SendFrageCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Guid))
            {
                return frageRepository.GetCurrentFrage();
            }

            return gameService.GetNextFrage(request.Guid);
        }
    }
}

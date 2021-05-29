using MediatR;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QuizPrototype.WebApi.Commands
{
    public class SendNextFrageCommandHandler : IRequestHandler<SendNextFrageCommand, Frage>
    {
        private readonly IFrageRepository frageRepository;
        private readonly IGameService gameService;

        public SendNextFrageCommandHandler(IFrageRepository frageRepository, IGameService gameService)
        {
            this.frageRepository = frageRepository;
            this.gameService = gameService;
        }

        public async Task<Frage> Handle(SendNextFrageCommand request, CancellationToken cancellationToken)
        {
            await gameService.UpdateGame(request.Guid, request.FrageId, request.Score);
            return await frageRepository.GetById(request.FrageId);
        }
    }
}

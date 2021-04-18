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

        public SendFrageCommandHandler(IFrageRepository frageRepository)
        {
            this.frageRepository = frageRepository;
        }

        public Task<Frage> Handle(SendFrageCommand request, CancellationToken cancellationToken)
        {
            return frageRepository.GetCurrentFrage();
        }
    }
}

using MediatR;
using QuizPrototype.WebApi.Models;
using QuizPrototype.WebApi.Repositories;
using System;
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
            return Task.FromResult(frageRepository.GetCurrentFrage());
        }
    }
}

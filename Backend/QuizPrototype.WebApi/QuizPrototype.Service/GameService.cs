using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace QuizPrototype.Service
{
    public class GameService : IGameService
    {
        private readonly IFrageRepository frageRepository;
        private readonly IGameRepository gameRepository;

        public GameService(IFrageRepository frageRepository, IGameRepository gameRepository)
        {
            this.frageRepository = frageRepository;
            this.gameRepository = gameRepository;
        }

        public async Task<Frage> GetNextFrage(string guid)
        {
            var currentGame = await gameRepository.GetByGuid(guid);
            currentGame.AktuelleFrageId++;
            var frage = await frageRepository.GetById(currentGame.AktuelleFrageId);
            if (frage == null)
            {
                // Loop to first Frage
                frage = await frageRepository.GetCurrentFrage();
                currentGame.AktuelleFrageId = frage.Id;
            }
            await gameRepository.UpdateGame(currentGame);
            return frage;
        }

        public async Task<Game> StartGame()
        {
            var game = new Game { Guid = Guid.NewGuid().ToString(), AktuelleFrageId = 0 };
            await gameRepository.CreateGame(game);
            return game;
        }
    }
}

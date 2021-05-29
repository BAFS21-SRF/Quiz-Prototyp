using QuizPrototype.Domain.Entities;
using System.Threading.Tasks;

namespace QuizPrototype.Domain.Interfaces
{
    public interface IGameService
    {
        Task<Game> StartGame();
        Task<Frage> GetNextFrage(string guid);
        Task UpdateGame(string guid, long frageId, long score);
    }
}

using QuizPrototype.Domain.Entities;
using System.Threading.Tasks;

namespace QuizPrototype.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task CreateGame(Game game);
        Task<Game> GetByGuid(string guid);
        Task UpdateGame(Game game);
    }
}

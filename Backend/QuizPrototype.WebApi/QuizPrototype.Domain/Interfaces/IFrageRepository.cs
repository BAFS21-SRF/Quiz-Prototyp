using QuizPrototype.Domain.Entities;
using System.Threading.Tasks;

namespace QuizPrototype.Domain.Interfaces
{
    public interface IFrageRepository
    {
        Task<Frage> GetCurrentFrage();
        Task<Frage> GetById(long id);
    }
}

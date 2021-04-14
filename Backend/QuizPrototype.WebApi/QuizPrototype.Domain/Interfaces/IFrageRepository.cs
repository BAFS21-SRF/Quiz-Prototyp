using QuizPrototype.Domain.Entities;

namespace QuizPrototype.Domain.Interfaces
{
    public interface IFrageRepository
    {
        Frage GetCurrentFrage();
    }
}

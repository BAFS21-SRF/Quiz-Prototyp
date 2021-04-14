using QuizPrototype.WebApi.Models;
using System.Collections.Generic;

namespace QuizPrototype.WebApi.Repositories
{
    public interface IFrageRepository
    {
        Frage GetCurrentFrage();
    }
}

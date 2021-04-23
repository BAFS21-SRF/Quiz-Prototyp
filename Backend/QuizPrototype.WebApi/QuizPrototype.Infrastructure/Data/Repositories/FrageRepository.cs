using Microsoft.EntityFrameworkCore;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using QuizPrototype.Infrastructure.Data.Context;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPrototype.Infrastructure.Data.Repositories
{
    public class FrageRepository : IFrageRepository
    {
        private readonly QuizPrototypeDbContext context;

        public FrageRepository(QuizPrototypeDbContext context)
        {
            this.context = context;
        }

        public async Task<Frage> GetById(long id)
        {
            var frage = await context.Frage.Where(f => f.Id == id).SingleOrDefaultAsync();
            if (frage != null)
            {
                frage.Auswahlmoeglichkeiten = await context.Auswahl.Where(x => x.FrageId == frage.Id).ToListAsync();
            }
            return frage;
        }

        public async Task<Frage> GetCurrentFrage()
        {
            var frage = await context.Frage.FirstOrDefaultAsync();
            if (frage != null)
            {
                frage.Auswahlmoeglichkeiten = await context.Auswahl.Where(x => x.FrageId == frage.Id).ToListAsync();
            }
            return frage;
        }
    }
}

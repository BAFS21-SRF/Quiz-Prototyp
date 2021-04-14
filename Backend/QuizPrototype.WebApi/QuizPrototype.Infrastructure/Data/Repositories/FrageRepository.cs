using Microsoft.EntityFrameworkCore;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizPrototype.Infrastructure.Data.Repositories
{
    public class FrageRepository : IFrageRepository
    {
        private readonly DbContext context;

        public FrageRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<Frage> GetCurrentFrage()
        {
            //return await context.Set<Frage>().FirstAsync();
            var auswahlMoeglichkeiten = new List<Auswahl> {
                new Auswahl { Id = 1, AuswahlText = "Maus", Order = 4 },
                new Auswahl { Id = 2, AuswahlText = "Elefant", Order = 1 },
                new Auswahl { Id = 3, AuswahlText = "Hund", Order = 3 },
                new Auswahl { Id = 4, AuswahlText = "Tiger", Order = 2 }
            };
            return await Task.FromResult(new Frage { Id = 1, FrageText = "Ordne die Tiere nach der Grösse", Auswahlmoeglichkeiten = auswahlMoeglichkeiten });
        }
    }
}

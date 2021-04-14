using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using System.Collections.Generic;

namespace QuizPrototype.Infrastructure.Data.Repositories
{
    public class FrageRepository : IFrageRepository
    {
        public Frage GetCurrentFrage()
        {
            var auswahlMoeglichkeiten = new List<Auswahl> {
                new Auswahl { Id = 1, AuswahlText = "Maus", Order = 4 },
                new Auswahl { Id = 2, AuswahlText = "Elefant", Order = 1 },
                new Auswahl { Id = 3, AuswahlText = "Hund", Order = 3 },
                new Auswahl { Id = 4, AuswahlText = "Tiger", Order = 2 }
            };
            return new Frage { Id = 1, FrageText = "Ordne die Tiere nach der Grösse", Auswahlmoeglichkeiten = auswahlMoeglichkeiten};
        }
    }
}

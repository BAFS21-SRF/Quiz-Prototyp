using System.Collections.Generic;

namespace QuizPrototype.Domain.Entities
{
    public class Frage : BaseEntity
    {
        public string FrageText { get; set; }
        public List<Auswahl> Auswahlmoeglichkeiten { get; set; }
    }
}

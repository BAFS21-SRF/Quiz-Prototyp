using System.Collections.Generic;

namespace QuizPrototype.WebApi.Models
{
    public class Frage
    {
        public int Id { get; set; }
        public string FrageText { get; set; }
        public List<Auswahl> Auswahlmoeglichkeiten { get; set; }
    }
}

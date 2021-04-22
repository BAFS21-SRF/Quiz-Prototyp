
namespace QuizPrototype.Domain.Entities
{
    public class Auswahl : BaseEntity
    {
        public string AuswahlText { get; set; }
        public int Order { get; set; }
        public long FrageId { get; set; }
        public string AssetId { get; set; }
    }
}

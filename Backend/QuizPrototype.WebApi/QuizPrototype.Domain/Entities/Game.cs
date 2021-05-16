
namespace QuizPrototype.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Guid { get; set; }
        public long AktuelleFrageId { get; set; }
        public long Score { get; set; }
    }
}

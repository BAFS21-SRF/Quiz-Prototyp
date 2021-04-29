using FluentMigrator;

namespace QuizPrototype.DbMigration.Migrations
{
    [Migration(1, "The initial migration")]
    public class M001_Initial : MigrationBase
    {
        public override void Up()
        {
            var scripts = new[]
            {
                GetFullEmbeddedName("M001_InitialSchema")
            };

            foreach (var script in scripts)
            {
                Execute.EmbeddedScript(script);
            }
        }
    }
}

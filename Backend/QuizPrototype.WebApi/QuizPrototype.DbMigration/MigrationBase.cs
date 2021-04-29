using FluentMigrator;

namespace QuizPrototype.DbMigration
{
    public abstract class MigrationBase : ForwardOnlyMigration
    {
        protected static string BaseNamespace => $"{nameof(QuizPrototype)}.{nameof(DbMigration)}";

        protected static string GetFullEmbeddedName(string sqlScriptName)
        {
            return $"{BaseNamespace}.Scripts.{sqlScriptName}.sql";
        }
    }
}




    


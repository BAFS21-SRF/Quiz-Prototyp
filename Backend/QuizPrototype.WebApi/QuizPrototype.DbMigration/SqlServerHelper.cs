using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace QuizPrototype.DbMigration
{
    public static class SqlServerHelper
    {
        private const int CommandTimeout = 900;
        private const int ConnectRetryCount = 5;
        private const int ConnectRetryInterval = 5;
        private const int ConnectTimeout = 30;

        public static IServiceProvider CreateServices(string connectionString, Assembly assembly)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2016()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(assembly)
                        .For.EmbeddedResources()
                        .For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        public static async Task EnsureDatabaseAsync(string connectionString, string dbPricingTier = "S3", string dbMaxsize = "250")
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            bool isLocal = (builder.DataSource == "db" || builder.DataSource.Contains("localhost"));
            var dbName = builder.InitialCatalog;

            Console.WriteLine($"Ensuring Database [{dbName}] exists...");

            builder.InitialCatalog = "master";
            builder.ConnectRetryCount = ConnectRetryCount;
            builder.ConnectRetryInterval = ConnectRetryInterval;
            builder.ConnectTimeout = ConnectTimeout;

            using (var connection = new SqlConnection(builder.ConnectionString)) {
                await connection.OpenAsync();

                var query = connection.CreateCommand();

                query.CommandText = $"SELECT COUNT(*) FROM sys.databases WHERE NAME = '{dbName}'";

                var result = await query.ExecuteScalarAsync();

                if ((int)result != 0)
                {
                    Console.WriteLine($"Database [{dbName}] already exists!");
                    return;
                }

                Console.WriteLine($"Creating Database [{dbName}].");

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"CREATE DATABASE [{dbName}]";

                    if (!isLocal)
                    {
                        cmd.CommandText = $"CREATE DATABASE [{dbName}]";
                    }

                    cmd.CommandTimeout = CommandTimeout;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static void EnsureDatabase(string connectionString)
        {
            EnsureDatabaseAsync(connectionString).Wait();
        }

        public static void OutputLog(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            if (runner.HasMigrationsToApplyUp())
            {
                runner.MigrateUp();
                OutputLog(ConsoleColor.Green, "Migration finished.");
                return;
            }

            OutputLog(ConsoleColor.Green, "Nothing to migrate! Database is up to date.");
        }
    }
    
}

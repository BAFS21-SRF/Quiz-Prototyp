using System;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizPrototype.DbMigration
{
    internal static class Program
    {
        private static IConfiguration configuration;

        private static void Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration["EMAIL_DB"];

            SqlServerHelper.OutputLog(ConsoleColor.DarkGray, $"Using connection string: '{connectionString}'");
            SqlServerHelper.EnsureDatabase(connectionString);

            var serviceProvider = SqlServerHelper.CreateServices(connectionString, Assembly.GetExecutingAssembly());

            using (var scope = serviceProvider.CreateScope())
            {
                SqlServerHelper.UpdateDatabase(scope.ServiceProvider);
            }
        }
    }
}

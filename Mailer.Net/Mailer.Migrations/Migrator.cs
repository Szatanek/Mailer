using System;
using System.Reflection;
using DbUp;

namespace Mailer.Migrations
{
    internal class Migrator
    {
        public static Exception UpgradeDatabase(string connectionString)
        {
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();
            return result.Error;
        }
    }
}
using System;
using Mailer.Migrations.Arguments;

namespace Mailer.Migrations
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var arguments = ApplicationArgumentsProvider.GetArguments(args);

            var result = Migrator.UpgradeDatabase(arguments.ConnectionString);
            if (result != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result);
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}

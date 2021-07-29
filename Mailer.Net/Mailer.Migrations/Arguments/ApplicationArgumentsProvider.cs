namespace Mailer.Migrations.Arguments
{
    internal class ApplicationArgumentsProvider
    {
        private const int ExpectedArguments = 1;

        internal static ApplicationArguments GetArguments(string[] args)
        {
            if (args.Length < ExpectedArguments)
            {
                throw new InvalidApplicationArgumentsException();
            }

            return new ApplicationArguments
            {
                ConnectionString = args[0],
            };
        }
    }
}
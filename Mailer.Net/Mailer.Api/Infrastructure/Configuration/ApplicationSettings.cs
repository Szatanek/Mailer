using Mailer.Infrastructure;

namespace Mailer.Api.Infrastructure.Configuration
{
    public sealed class ApplicationSettings : IMailerDependencySettings
    {
        public ApplicationSettings(string connectionString, string smtpHostAddress, int mailRetryIntervalMilliseconds, string smtpLogin, string smtpPassword)
        {
            ConnectionString = connectionString;
            SmtpHostAddress = smtpHostAddress;
            MailRetryIntervalMilliseconds = mailRetryIntervalMilliseconds;
            SmtpLogin = smtpLogin;
            SmtpPassword = smtpPassword;
        }

        public string ConnectionString { get; }

        public string SmtpHostAddress { get; }

        public int MailRetryIntervalMilliseconds { get; }

        public string SmtpLogin { get; }

        public string SmtpPassword { get; }
    }
}

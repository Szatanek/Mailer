using Mailer.Infrastructure;

namespace Mailer.Services.Tests.Infrastructure
{
    public sealed class TestSettings : IMailerDependencySettings
    {
        public string ConnectionString { get; set; }

        public string SmtpHostAddress { get; set; }

        public int MailRetryIntervalMilliseconds { get; set; }

        public string SmtpLogin { get; set; }

        public string SmtpPassword { get; set; }
    }
}
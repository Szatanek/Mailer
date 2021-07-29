namespace Mailer.Infrastructure
{
    public interface IMailerDependencySettings
    {
        string ConnectionString { get; }

        string SmtpHostAddress { get; }

        string SmtpLogin { get; }

        string SmtpPassword { get; }

        int MailRetryIntervalMilliseconds { get; }
    }
}

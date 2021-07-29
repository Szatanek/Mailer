using Microsoft.Extensions.Configuration;

namespace Mailer.Api.Infrastructure.Configuration
{
    internal static class ApplicationConfigurationProvider
    {
        public static ApplicationSettings GetConfiguration(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Mailer");
            var smtpAddress = configuration.GetSection("SmtpHostAddress").Get<string>();
            var retryMilliseconds = configuration.GetSection("MailRetryIntervalMilliseconds").Get<int>();
            var login = configuration.GetSection("SmtpLogin").Get<string>();
            var password = configuration.GetSection("SmtpPassword").Get<string>();
            return new ApplicationSettings(connectionString, smtpAddress, retryMilliseconds, login, password);
        }
    }
}
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Mailer.Domain.Mail;

namespace Mailer.Infrastructure.Clients
{
    public sealed class MailSmtpClient : IMailClient
    {
        private readonly SmtpClient smtpClient;

        public MailSmtpClient(IMailerDependencySettings settings)
        {
            var credential = string.IsNullOrEmpty(settings.SmtpPassword)
                ? CredentialCache.DefaultNetworkCredentials
                : new NetworkCredential(settings.SmtpLogin, settings.SmtpPassword);

            smtpClient = new SmtpClient(settings.SmtpHostAddress)
            {
                Credentials = credential,
            };
        }

        public async Task SendAsync(MailEntity mail, CancellationToken cancellationToken)
        {
            var message = new MailMessage
            {
                From = new System.Net.Mail.MailAddress((string)mail.Sender),
                Subject = (string)mail.Topic,
                Body = mail.Body.Content,
                IsBodyHtml = mail.Body.IsHtml,
            };

            foreach(var address in mail.Recipient.MailAddresses)
            {
                message.To.Add((string)address);
            }

            try
            {
                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new SendMailException(ex);
            }
        }
    }
}

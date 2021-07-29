using System.Threading;
using System.Threading.Tasks;

namespace Mailer.Domain.Mail
{
    public sealed class SendMailService
    {
        private readonly IMailRepository mailRepository;
        private readonly IMailIntervalPolicy mailIntervalPolicy;
        private readonly IMailClient mailClient;

        public SendMailService(
            IMailRepository mailRepository,
            IMailIntervalPolicy mailIntervalPolicy,
            IMailClient mailClient)
        {
            this.mailRepository = mailRepository;
            this.mailIntervalPolicy = mailIntervalPolicy;
            this.mailClient = mailClient;
        }

        public async Task SendMailAsync(MailGuid mailId, CancellationToken cancellationToken)
        {
            var mail = await mailRepository.GetAsync(mailId, cancellationToken);
            if (mail.Status == MailStatus.Sent)
            {
                throw new MailAlreadySentException();
            }

            await mailIntervalPolicy.WaitUntilNextSendRequest(mail);

            try
            {
                await mailClient.SendAsync(mail, cancellationToken);
                mail.MailSent();
            }
            catch (SendMailException ex)
            {
                mail.AddErrorResponse(ex.SendErrorMessage, ex.SendErrorType);
            }

            await mailRepository.SaveAsync(mail, cancellationToken);
        }
    }
}
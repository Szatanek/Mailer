using System.Threading.Tasks;
using Mailer.Domain.Mail;

namespace Mailer.Infrastructure.Services
{
    public sealed class MailIntervalPolicy : IMailIntervalPolicy
    {
        private readonly int intervalMilliseconds;

        public MailIntervalPolicy(IMailerDependencySettings settings)
        {
            intervalMilliseconds = settings.MailRetryIntervalMilliseconds;
        }

        public async Task WaitUntilNextSendRequest(MailEntity mail)
        {
            if (mail.Status == MailStatus.New)
            {
                return;
            }

            await Task.Delay(intervalMilliseconds);
        }
    }
}

using System.Threading.Tasks;

namespace Mailer.Domain.Mail
{
    public interface IMailIntervalPolicy
    {
        Task WaitUntilNextSendRequest(MailEntity mail);
    }
}

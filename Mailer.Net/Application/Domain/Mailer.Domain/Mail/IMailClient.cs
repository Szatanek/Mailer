using System.Threading;
using System.Threading.Tasks;

namespace Mailer.Domain.Mail
{
    public interface IMailClient
    {
        Task SendAsync(MailEntity mail, CancellationToken cancellationToken);
    }
}
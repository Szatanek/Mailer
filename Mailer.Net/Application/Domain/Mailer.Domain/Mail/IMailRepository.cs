using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mailer.Domain.System;

namespace Mailer.Domain.Mail
{
    public interface IMailRepository
    {
        Task SaveAsync(MailEntity mailEntity, CancellationToken cancellationToken);

        Task<MailEntity> GetAsync(MailGuid mailId, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<MailEntity>> GetAllAsync(SystemId systemId, CancellationToken cancellationToken);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Services.Query;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Mailer.Services.Contracts.Read.Queries;
using Mailer.Services.Contracts.Read.Views;

namespace Mailer.Services.Mail
{
    public sealed class GetMailsQueryHandler : IAsyncQueryHandler<GetMailsQuery>
    {
        private readonly IMailRepository mailRepository;

        public GetMailsQueryHandler(IMailRepository mailRepository)
        {
            this.mailRepository = mailRepository;
        }

        public async Task<T> HandleAsync<T>(GetMailsQuery query, CancellationToken cancellationToken)
        {
            var mails = await GetMailsAsync(query.SystemId, cancellationToken);
            return (T)mails;
        }

        private async Task<IEnumerable<MailReadViewModel>> GetMailsAsync(int systemId, CancellationToken cancellationToken)
        {
            var mails = await mailRepository.GetAllAsync((SystemId)systemId, cancellationToken);
            return mails.Select(m => new MailReadViewModel
            {
                Guid = (Guid)m.MailGuid,
                From = (string)m.Sender,
                To = (string)m.Recipient,
                Status = m.Status.ToString(),
                Topic = (string)m.Topic,
                Timestamp = m.Timestamp,
            });
        }
    }
}

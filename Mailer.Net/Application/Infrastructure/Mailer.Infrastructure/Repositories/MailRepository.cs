using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Domain;
using Framework.Services.Event;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Mailer.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Mailer.Infrastructure.Repositories
{
    public sealed class MailRepository : IMailRepository
    {
        private readonly MailerContext context;
        private readonly IEventQueue eventQueue;

        public MailRepository(MailerContext context, IEventQueue eventQueue)
        {
            this.context = context;
            this.eventQueue = eventQueue;
        }

        public async Task<MailEntity> GetAsync(MailGuid mailId, CancellationToken cancellationToken)
        {
            var mail = await context.Mails
                .Include(m => m.Responses)
                .SingleOrDefaultAsync(m => m.MailGuid == mailId);

            if (mail == null)
            {
                throw new MailNotFoundException();
            }

            return mail;
        }

        public async Task SaveAsync(MailEntity mailEntity, CancellationToken cancellationToken)
        {
            if (mailEntity.Id == 0)
            {
                context.Mails.Add(mailEntity);
            }
            else
            {
                context.Mails.Update(mailEntity);
            }

            await context.SaveChangesAsync(cancellationToken);
            HandleEvents(mailEntity.Events.ToArray());
        }

        public async Task<IReadOnlyCollection<MailEntity>> GetAllAsync(SystemId systemId, CancellationToken cancellationToken)
        {
            return await context.Mails
                .Where(m => m.SystemId == systemId)
                .ToListAsync(cancellationToken);
        }

        private void HandleEvents(IReadOnlyCollection<IDomainEvent> events)
        {
            eventQueue.EnqueueEvents(events);
        }
    }
}

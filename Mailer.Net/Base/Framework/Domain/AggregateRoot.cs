using System.Collections.Generic;
using System.Diagnostics;

namespace Framework.Domain
{
    public abstract class AggregateRoot : DomainEntity
    {
        private readonly Queue<IDomainEvent> events = new Queue<IDomainEvent>();

        public IEnumerable<IDomainEvent> Events => events.ToArray();

        [DebuggerStepThrough]
        protected void Enqueue(IDomainEvent @event)
        {
            if (@event.GetType() != typeof(NullObjectEvent))
            {
                events.Enqueue(@event);
            }
        }
    }
}
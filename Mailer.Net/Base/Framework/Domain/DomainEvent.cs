using System;
using System.Diagnostics;

namespace Framework.Domain
{
    public abstract class DomainEvent<T> : IDomainEvent
    {
        [DebuggerStepThrough]
        protected DomainEvent(T aggregateId, Guid? id = null)
        {
            AggregateId = aggregateId;
            Id = id ?? Guid.NewGuid();
        }

        public T AggregateId { get; }

        string IDomainEvent.AggregateId => AggregateId.ToString();

        public Guid Id { get; }
    }
}
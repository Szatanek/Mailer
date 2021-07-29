using System;

namespace Framework.Domain
{
    public sealed class NullObjectEvent : IDomainEvent
    {
        public NullObjectEvent()
        {
            Id = Guid.Empty;
        }

        public Guid Id { get; }

        public string AggregateId => Id.ToString();
    }
}
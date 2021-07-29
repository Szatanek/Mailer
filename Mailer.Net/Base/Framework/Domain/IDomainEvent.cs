using System;

namespace Framework.Domain
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        string AggregateId { get; }
    }
}
using System.Collections.Generic;
using Framework.Domain;

namespace Framework.Services.Event
{
    public interface IEventQueue
    {
        bool HasEvents { get; }

        IDomainEvent Dequeue();

        void Enqueue<T>(T @event) where T : class, IDomainEvent;

        void EnqueueEvents(IEnumerable<IDomainEvent> events);
    }
}
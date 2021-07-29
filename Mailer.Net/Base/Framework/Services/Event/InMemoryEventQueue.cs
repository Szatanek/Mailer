using Framework.Domain;
using System.Collections.Generic;

namespace Framework.Services.Event
{
    public sealed class InMemoryEventQueue : IEventQueue
    {
        private readonly Queue<IDomainEvent> Events = new Queue<IDomainEvent>();

        public bool HasEvents => Events.Count != 0;

        public void Enqueue<T>(T @event)
            where T : class, IDomainEvent
        {
            Events.Enqueue(@event);
        }

        public IDomainEvent Dequeue()
        {
            return Events.Dequeue();
        }

        public void EnqueueEvents(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Enqueue(@event);
            }
        }
    }
}
using Framework.Domain;
using Framework.Services.Event;

namespace Framework.Services
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T: AggregateRoot
    {
        private readonly IEventQueue eventQueue;
        private readonly EventStore eventStore;

        protected BaseRepository(IEventQueue eventQueue, EventStore eventStore)
        {
            this.eventQueue = eventQueue;
            this.eventStore = eventStore;
        }

        protected void HandleEvents(T aggregate)
        {
            eventStore.StoreEvents(aggregate);
            eventQueue.EnqueueEvents(aggregate.Events);
        }
    }
}

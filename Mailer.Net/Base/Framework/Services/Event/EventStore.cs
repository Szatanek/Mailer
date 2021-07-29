using Framework.Domain;
using Framework.Utils;

namespace Framework.Services.Event
{
    public sealed class EventStore
    {
        private readonly IEventSerializer eventSerializer;
        private readonly IEventRepository eventRepository;

        public EventStore(IEventSerializer eventSerializer, IEventRepository eventRepository)
        {
            this.eventSerializer = eventSerializer;
            this.eventRepository = eventRepository;
        }

        public void StoreEvents<TAggregate>(TAggregate aggregate)
            where TAggregate : AggregateRoot
        {
            foreach (var @event in aggregate.Events)
            {
                var eventRow = new EventPoco
                {
                    Id = @event.Id,
                    AggregateId = @event.AggregateId,
                    AggregateType = aggregate.GetType().Name,
                    EventType = @event.GetType().Name,
                    EventData = eventSerializer.Serialize(@event),
                    Timestamp = DateTimeProvider.Current.Now
                };

                eventRepository.Add(eventRow);
            }

            eventRepository.SaveChanges();
        }
    }
}

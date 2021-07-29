using Framework.Domain;
using Framework.Services.Event;
using Framework.Utils;

namespace Framework.Infrastructure.EventStore
{
    public sealed class JsonEventSerializer : IEventSerializer
    {
        public string Serialize(IDomainEvent @event)
        {
            return SerializationProvider.Current.Serialize(@event);
        }
    }
}

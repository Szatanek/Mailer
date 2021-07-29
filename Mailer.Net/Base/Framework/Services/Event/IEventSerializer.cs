using Framework.Domain;

namespace Framework.Services.Event
{
    public interface IEventSerializer
    {
        string Serialize(IDomainEvent @event);
    }
}
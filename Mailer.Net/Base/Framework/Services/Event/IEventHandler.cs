using Framework.Domain;

namespace Framework.Services.Event
{
    public interface IEventHandler
    {
        void Handle(IDomainEvent @event);
    }
}
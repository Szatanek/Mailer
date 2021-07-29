using Framework.Domain;

namespace Framework.Services.Event
{
    public interface IEventHandler<T> : IEventHandler
        where T : class, IDomainEvent
    {
        void Handle(T @event);
    }
}
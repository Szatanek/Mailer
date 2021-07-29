using Framework.Domain;

namespace Framework.Services.Event
{
    public interface IEventHandlerRegistrar
    {
        IEventHandler GetHandler<TEvent>(TEvent @event) where TEvent : class, IDomainEvent;
    }
}
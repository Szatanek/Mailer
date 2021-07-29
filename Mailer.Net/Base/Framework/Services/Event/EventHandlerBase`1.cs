using Framework.Domain;

namespace Framework.Services.Event
{
    public abstract class EventHandlerBase<T> : IEventHandler<T>
        where T : class, IDomainEvent
    {
        public abstract void Handle(T @event);

        void IEventHandler<T>.Handle(T @event)
        {
            Handle(@event);
        }

        void IEventHandler.Handle(IDomainEvent @event)
        {
            Handle(@event as T);
        }
    }
}

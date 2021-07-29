using System.Linq;
using System.Reflection;
using Framework.Domain;
using Framework.Services.Event;
using SimpleInjector;

namespace Framework.Infrastructure.SimpleInjector
{
    public sealed class SimpleInjectorEventHandlerRegistrar : IEventHandlerRegistrar
    {
        private readonly Container container;

        private SimpleInjectorEventHandlerRegistrar(Container container)
        {
            this.container = container;
        }

        public IEventHandler GetHandler<TEvent>(TEvent @event)
            where TEvent : class, IDomainEvent
        {
            var eventHandlerType = typeof(IEventHandler);
            var eventType = @event.GetType();
            var eventHandlers = container.GetCurrentRegistrations()
                .Where(t => eventHandlerType.IsAssignableFrom(t.ServiceType))
                .ToList();
            var producer = eventHandlers
                .FirstOrDefault(t => t.ServiceType.GetGenericArguments().Contains(eventType));

            return producer.GetInstance() as IEventHandler;
        }

        public static void RegisterEventHandling(Container container, params Assembly[] assemblies)
        {
            container.Register(typeof(IEventHandler<>), assemblies);
            container.Register<IEventHandlerRegistrar>(() => new SimpleInjectorEventHandlerRegistrar(container), Lifestyle.Singleton);
        }
    }
}

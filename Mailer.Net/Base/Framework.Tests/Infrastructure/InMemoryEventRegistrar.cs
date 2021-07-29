using Framework.Domain;
using Framework.Services.Event;
using System;
using System.Collections.Generic;

namespace Framework.Tests.Infrastructure
{
    public sealed class InMemoryEventRegistrar : IEventHandlerRegistrar
    {
        private static readonly Dictionary<Type, IEventHandler> Handlers = new Dictionary<Type, IEventHandler>();

        public IEventHandler GetHandler<T>(T @event)
            where T : class, IDomainEvent
        {
            return Handlers[typeof(T)];
        }

        public void RegisterHandler<T>(IEventHandler<T> handler)
            where T : class, IDomainEvent
        {
            Handlers.Add(typeof(T), handler);
        }
    }
}
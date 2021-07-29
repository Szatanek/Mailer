using System.Reflection;
using Framework.Application;
using Framework.Services.Query;
using SimpleInjector;

namespace Framework.Infrastructure.SimpleInjector
{
    public sealed class SimpleInjectorQueryHandlerRegistrar : IQueryHandlerRegistrar
    {
        private readonly Container container;

        private SimpleInjectorQueryHandlerRegistrar(Container container)
        {
            this.container = container;
        }

        public IQueryHandler<TQuery> GetHandler<TQuery>()
            where TQuery : class, IApplicationQuery
        {
            return container.GetInstance<IQueryHandler<TQuery>>();
        }

        public IAsyncQueryHandler<TQuery> GetAsyncHandler<TQuery>()
            where TQuery : class, IApplicationQuery
        {
            return container.GetInstance<IAsyncQueryHandler<TQuery>>();
        }

        public static void RegisterQueryDispatcher(Container container, params Assembly[] assemblies)
        {
            container.Register<IApplicationQueryDispatcher, ApplicationQueryDispatcher>();
            container.Register(typeof(IQueryHandler<>), assemblies);
            container.Register(typeof(IAsyncQueryHandler<>), assemblies);
            container.Register<IQueryHandlerRegistrar>(() => new SimpleInjectorQueryHandlerRegistrar(container), Lifestyle.Singleton);
        }
    }
}

using System.Reflection;
using Framework.Application;
using Framework.Services.Command;
using SimpleInjector;

namespace Framework.Infrastructure.SimpleInjector
{
    public sealed class SimpleInjectorCommandHandlerRegistrar : ICommandHandlerRegistrar
    {
        private readonly Container container;

        private SimpleInjectorCommandHandlerRegistrar(Container container)
        {
            this.container = container;
        }

        public IAsyncCommandHandler<TCommand> GetAsyncHandler<TCommand>()
            where TCommand : class, IApplicationCommand
        {
            return container.GetInstance<IAsyncCommandHandler<TCommand>>();
        }

        public ICommandHandler<TCommand> GetHandler<TCommand>()
            where TCommand : class, IApplicationCommand
        {
            return container.GetInstance<ICommandHandler<TCommand>>();
        }

        public static void RegisterCommandBus(Container container, params Assembly[] assemblies)
        {
            container.Register<IApplicationCommandBus, CommandBus>();
            container.Register(typeof(ICommandHandler<>), assemblies);
            container.Register(typeof(IAsyncCommandHandler<>), assemblies);
            container.Register<ICommandHandlerRegistrar>(() => new SimpleInjectorCommandHandlerRegistrar(container), Lifestyle.Singleton);
        }
    }
}

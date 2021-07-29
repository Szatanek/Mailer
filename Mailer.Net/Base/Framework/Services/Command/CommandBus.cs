using System.Threading;
using System.Threading.Tasks;
using Framework.Application;

namespace Framework.Services.Command
{
    public sealed class CommandBus : IApplicationCommandBus
    {
        private readonly ICommandHandlerRegistrar commandHandlerRegistrar;

        public CommandBus(ICommandHandlerRegistrar commandHandlerRegistrar)
        {
            this.commandHandlerRegistrar = commandHandlerRegistrar;
        }

        public Task HandleAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : class, IApplicationCommand
        {
            var commandHandler = commandHandlerRegistrar.GetAsyncHandler<TCommand>();
            return commandHandler.HandleAsync(command, cancellationToken);
        }

        public void Handle<TCommand>(TCommand command)
            where TCommand : class, IApplicationCommand
        {
            var commandHandler = commandHandlerRegistrar.GetHandler<TCommand>();
            commandHandler.Handle(command);
        }
    }
}

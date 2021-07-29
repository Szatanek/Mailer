using Framework.Application;

namespace Framework.Services.Command
{
    public interface ICommandHandlerRegistrar
    {
        ICommandHandler<TCommand> GetHandler<TCommand>()
            where TCommand : class, IApplicationCommand;

        IAsyncCommandHandler<TCommand> GetAsyncHandler<TCommand>()
            where TCommand : class, IApplicationCommand;
    }
}

using Framework.Application;

namespace Framework.Services.Command
{
    public interface ICommandHandler<TCommand>
        where TCommand : class, IApplicationCommand
    {
        void Handle(TCommand command);
    }
}
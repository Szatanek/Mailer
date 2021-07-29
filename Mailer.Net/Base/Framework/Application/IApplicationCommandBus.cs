using System.Threading;
using System.Threading.Tasks;
using Framework.Application;

namespace Framework.Services.Command
{
    public interface IApplicationCommandBus
    {
        void Handle<TCommand>(TCommand command)
            where TCommand : class, IApplicationCommand;

        Task HandleAsync<T>(T command, CancellationToken cancellationToken)
            where T : class, IApplicationCommand;
    }
}

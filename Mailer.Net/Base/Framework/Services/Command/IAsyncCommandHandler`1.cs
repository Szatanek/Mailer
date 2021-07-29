using System.Threading;
using System.Threading.Tasks;
using Framework.Application;

namespace Framework.Services.Command
{
    public interface IAsyncCommandHandler<TCommand>
        where TCommand : class, IApplicationCommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
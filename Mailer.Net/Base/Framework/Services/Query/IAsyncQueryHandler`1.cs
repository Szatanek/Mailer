using System.Threading;
using System.Threading.Tasks;
using Framework.Application;

namespace Framework.Services.Query
{
    public interface IAsyncQueryHandler<TQuery>
        where TQuery : IApplicationQuery
    {
        Task<T> HandleAsync<T>(TQuery query, CancellationToken cancellationToken);
    }
}
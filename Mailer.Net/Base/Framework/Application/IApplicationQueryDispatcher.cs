using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface IApplicationQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(TQuery query)
            where TQuery : class, IApplicationQuery;

        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : class, IApplicationQuery;
    }
}

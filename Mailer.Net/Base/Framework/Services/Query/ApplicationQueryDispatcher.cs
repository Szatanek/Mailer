using System.Threading;
using System.Threading.Tasks;
using Framework.Application;

namespace Framework.Services.Query
{
    public sealed class ApplicationQueryDispatcher : IApplicationQueryDispatcher
    {
        private readonly IQueryHandlerRegistrar queryHandlerRegistrar;

        public ApplicationQueryDispatcher(IQueryHandlerRegistrar queryHandlerRegistrar)
        {
            this.queryHandlerRegistrar = queryHandlerRegistrar;
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query)
            where TQuery : class, IApplicationQuery
        {
            var queryHandler = queryHandlerRegistrar.GetHandler<TQuery>();
            return queryHandler.Handle<TResult>(query);
        }

        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : class, IApplicationQuery
        {
            var queryHandler = queryHandlerRegistrar.GetAsyncHandler<TQuery>();
            return queryHandler.HandleAsync<TResult>(query, cancellationToken);
        }
    }
}

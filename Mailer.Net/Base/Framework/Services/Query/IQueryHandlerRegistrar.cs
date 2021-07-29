using Framework.Application;

namespace Framework.Services.Query
{
    public interface IQueryHandlerRegistrar
    {
        IQueryHandler<TQuery> GetHandler<TQuery>()
            where TQuery : class, IApplicationQuery;

        IAsyncQueryHandler<TQuery> GetAsyncHandler<TQuery>()
            where TQuery : class, IApplicationQuery;
    }
}
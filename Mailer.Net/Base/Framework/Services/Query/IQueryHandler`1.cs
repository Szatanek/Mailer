using Framework.Application;

namespace Framework.Services.Query
{
    public interface IQueryHandler<TQuery>
        where TQuery : IApplicationQuery
    {
        T Handle<T>(TQuery query);
    }
}
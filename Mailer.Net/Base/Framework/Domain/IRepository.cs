namespace Framework.Domain
{
    public interface IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
    }
}
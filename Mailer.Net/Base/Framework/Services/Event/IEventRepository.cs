namespace Framework.Services.Event
{
    public interface IEventRepository
    {
        void Add(EventPoco eventRow);

        void SaveChanges();
    }
}
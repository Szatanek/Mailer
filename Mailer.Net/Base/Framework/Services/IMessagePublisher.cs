namespace Framework.Services
{
    public interface IMessagePublisher
    {
        void Publish<T>(T message);
    }
}

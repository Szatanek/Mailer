using Framework.Services;
using System.Collections.Generic;

namespace Framework.Tests.Infrastructure
{
    public sealed class InMemoryMessagePublisher : IMessagePublisher
    {
        private readonly IList<object> messages = new List<object>();

        public int Count => messages.Count;

        public void Publish<T>(T message)
        {
            messages.Add(message);
        }
    }
}
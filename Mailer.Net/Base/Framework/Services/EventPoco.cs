using System;

namespace Framework.Services
{
    public sealed class EventPoco
    {
        public Guid Id { get; set; }

        public string AggregateId { get; set; }

        public string AggregateType { get; set; }

        public string EventType { get; set; }

        public string EventData { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
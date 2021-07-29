namespace Framework.Services.Event
{
    public sealed class EventProcessor
    {
        private readonly IEventQueue queue;
        private readonly IEventHandlerRegistrar eventRegister;

        public EventProcessor(IEventQueue queue, IEventHandlerRegistrar eventRegister)
        {
            this.queue = queue;
            this.eventRegister = eventRegister;
        }

        public void Process()
        {
            if (queue.HasEvents)
            {
                var @event = queue.Dequeue();
                var handler = eventRegister.GetHandler(@event);
                if (handler != null)
                {
                    handler.Handle(@event);
                }
            }
        }
    }
}
using System;

namespace Framework.Application
{
    public abstract class ApplicationCommand : IApplicationCommand
    {
        protected ApplicationCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}
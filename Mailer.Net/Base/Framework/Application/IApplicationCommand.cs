using System;

namespace Framework.Application
{
    public interface IApplicationCommand
    {
        public Guid Id { get; }
    }
}

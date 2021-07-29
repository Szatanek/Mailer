using System;

namespace Framework.Application
{
    public abstract class ApplicationQuery : IApplicationQuery
    {
        public ApplicationQuery()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}

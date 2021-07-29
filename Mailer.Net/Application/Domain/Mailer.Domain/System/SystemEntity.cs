using Framework.Domain;

namespace Mailer.Domain.System
{
    public sealed class SystemEntity : AggregateRoot
    {
        private SystemEntity()
        {
        }

        public SystemId Id { get; private set; }

        public string Name { get; private set; }

        public static SystemEntity Reconstitute(SystemId id, string name) =>
            new SystemEntity
            {
                Id = id,
                Name = name,
            };
    }
}

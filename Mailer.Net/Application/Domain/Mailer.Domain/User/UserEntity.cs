using Framework.Domain;

namespace Mailer.Domain.User
{
    public sealed class UserEntity : AggregateRoot
    {
        private UserEntity()
        {
        }

        public int Id { get; private set; }

        public UserLogin Login { get; private set; }

        public UserName Name { get; private set; }
    }
}

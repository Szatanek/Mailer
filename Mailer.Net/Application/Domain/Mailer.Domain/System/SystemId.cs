using Framework.Domain;

namespace Mailer.Domain.System
{
    public sealed class SystemId : ValueObject<int, SystemId>
    {
        private SystemId(int value)
            : base(value)
        {
        }

        public static explicit operator SystemId(int value)
        {
            return new SystemId(value);
        }
    }
}
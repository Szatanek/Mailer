using Framework.Domain;

namespace Mailer.Domain.User
{
    public sealed class UserLogin : ValueObject<string, UserLogin>
    {
        private UserLogin(string value)
            : base(value)
        {
        }

        public static explicit operator UserLogin(string value)
        {
            return new UserLogin(value);
        }
    }
}

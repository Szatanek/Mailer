using Framework.Domain;

namespace Mailer.Domain.User
{
    public sealed class UserName : ValueObject<UserName>
    {
        private UserName()
        {
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Value => $"{FirstName} {LastName}";

        public static explicit operator string(UserName userName)
        {
            return userName.Value;
        }

        public static UserName New(string firstName, string lastName)
        {
            return new UserName
            {
                FirstName = firstName,
                LastName = lastName
            };
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return !(obj is null)
                && obj.GetHashCode() == GetHashCode();
        }
    }
}
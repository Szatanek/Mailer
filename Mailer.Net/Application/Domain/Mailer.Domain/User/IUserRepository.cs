namespace Mailer.Domain.User
{
    public interface IUserRepository
    {
        UserEntity Get(UserLogin login);
    }
}

using Mailer.Domain.User;
using Mailer.Services.Contracts.Read.Queries;
using Mailer.Services.Contracts.Read.Views;

namespace Mailer.Services
{
    public sealed class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserDetailsViewModel GetUserDetails(GetUserDetailsQuery query)
        {
            var user = userRepository.Get((UserLogin)query.Login);
            return new UserDetailsViewModel
            {
                Id = user.Id,
                Login = (string)user.Login,
                Name = (string)user.Name,
            };
        }
    }
}
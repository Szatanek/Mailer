using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Mailer.Domain.User;
using Mailer.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Mailer.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly MailerContext context;

        public UserRepository(MailerContext context)
        {
            this.context = context;
        }

        public UserEntity Get(UserLogin login)
        {
            var user = context.Users
                .AsNoTracking()
                .SingleOrDefault(u => u.Login == login);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }
    }
}

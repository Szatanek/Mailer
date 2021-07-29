using System.Data;
using Dapper;
using Mailer.Domain.User;

namespace Mailer.Services.Tests.Infrastructure.TypeHandlers
{
    internal sealed class UserLoginTypeHandler : SqlMapper.TypeHandler<UserLogin>
    {
        public override UserLogin Parse(object value)
        {
            return (UserLogin)value.ToString();
        }

        public override void SetValue(IDbDataParameter parameter, UserLogin value)
        {
            parameter.Value = (string)value;
        }
    }
}

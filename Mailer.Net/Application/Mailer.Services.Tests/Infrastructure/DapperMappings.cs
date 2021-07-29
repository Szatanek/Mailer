using Dapper;
using Mailer.Services.Tests.Infrastructure.TypeHandlers;

namespace Mailer.Services.Tests.Infrastructure
{
    internal sealed class DapperMappings
    {
        public static void MapHandlers()
        {
            SqlMapper.AddTypeHandler(new UserLoginTypeHandler());
            SqlMapper.AddTypeHandler(new MailAddressTypeHandler());
            SqlMapper.AddTypeHandler(new MailGuidTypeHandler());
            SqlMapper.AddTypeHandler(new MailRecipientTypeHandler());
            SqlMapper.AddTypeHandler(new MailTopicTypeHandler());
            SqlMapper.AddTypeHandler(new MailStatusTypeHandler());
        }
    }
}

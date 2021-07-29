using System.Data;
using Dapper;
using Mailer.Domain.Mail;

namespace Mailer.Services.Tests.Infrastructure.TypeHandlers
{
    internal sealed class MailTopicTypeHandler : SqlMapper.TypeHandler<MailTopic>
    {
        public override MailTopic Parse(object value)
        {
            return (MailTopic)value.ToString();
        }

        public override void SetValue(IDbDataParameter parameter, MailTopic value)
        {
            parameter.Value = (string)value;
        }
    }
}

using Dapper;
using Mailer.Domain.Mail;
using System.Data;

namespace Mailer.Services.Tests.Infrastructure.TypeHandlers
{
    internal sealed class MailRecipientTypeHandler : SqlMapper.TypeHandler<MailRecipient>
    {
        public override MailRecipient Parse(object value)
        {
            return (MailRecipient)value.ToString();
        }

        public override void SetValue(IDbDataParameter parameter, MailRecipient value)
        {
            parameter.Value = (string)value;
        }
    }
}

using Dapper;
using Mailer.Domain.Mail;
using System.Data;

namespace Mailer.Services.Tests.Infrastructure.TypeHandlers
{
    internal sealed class MailAddressTypeHandler : SqlMapper.TypeHandler<MailAddress>
    {
        public override MailAddress Parse(object value)
        {
            return (MailAddress)value.ToString();
        }

        public override void SetValue(IDbDataParameter parameter, MailAddress value)
        {
            parameter.Value = (string)value;
        }
    }
}

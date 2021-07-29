using System;
using System.Data;
using Dapper;
using Mailer.Domain.Mail;

namespace Mailer.Services.Tests.Infrastructure.TypeHandlers
{
    internal sealed class MailStatusTypeHandler : SqlMapper.TypeHandler<MailStatus>
    {
        public override MailStatus Parse(object value)
        {
            return (MailStatus)Convert.ToByte(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, MailStatus value)
        {
            parameter.Value = (byte)value;
        }
    }
}

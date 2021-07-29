using System;
using System.Data;
using Dapper;
using Mailer.Domain.Mail;

namespace Mailer.Services.Tests.Infrastructure.TypeHandlers
{
    internal sealed class MailGuidTypeHandler : SqlMapper.TypeHandler<MailGuid>
    {
        public override MailGuid Parse(object value)
        {
            return (MailGuid)Guid.Parse(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, MailGuid value)
        {
            parameter.Value = (Guid)value;
        }
    }
}

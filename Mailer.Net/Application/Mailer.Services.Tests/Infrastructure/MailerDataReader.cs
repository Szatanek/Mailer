using Dapper;
using System.Collections.Generic;
using Framework.Services;
using System.Data.SqlClient;
using System;
using Mailer.Domain.Mail;
using System.Linq;

namespace Mailer.Services.Tests.Infrastructure
{
    internal sealed class MailerDataReader
    {
        private readonly string connectionString;

        public MailerDataReader(TestSettings testSettings)
        {
            connectionString = testSettings.ConnectionString;
            DapperMappings.MapHandlers();
        }

        internal IEnumerable<EventPoco> GetEvents()
        {
            var sql = @"SELECT [e_guid] AS Id
                              ,[e_aggregateId] AS AggregateId
                              ,[e_aggregateType] AS AggregateType
                              ,[e_eventType] AS EventType
                              ,[e_eventData] AS EventData
                              ,[e_timestamp] AS Timestamp
                          FROM [etiPack].[t_event]";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<EventPoco>(sql);
            }
        }

        internal IReadOnlyCollection<MailResponseEntity> GetMailResponses(Guid mailGuid)
        {
            var sql = @"SELECT MR.*
                        FROM [mailer].[MailResponses] MR
                        JOIN [mailer].[Mails] M ON MR.MailId = M.Id
                        WHERE M.MailGuid = @mailGuid";

            var parameters = new { mailGuid };
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<MailResponseEntity>(sql, parameters).AsList();
            }
        }

        internal MailEntity GetMail(Guid mailGuid)
        {
            var sql = @"SELECT TOP 1 *
                          FROM [mailer].[Mails]
                          WHERE MailGuid = @mailGuid";

            var parameters = new { mailGuid };
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<MailEntity>(sql, parameters);
            }
        }

        internal MailBody GetMailBody(Guid mailGuid)
        {
            var sql = @"SELECT TOP 1 Content, IsHtml
                          FROM [mailer].[Mails]
                          WHERE MailGuid = @mailGuid";

            var parameters = new { mailGuid };
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<MailBody>(sql, parameters);
            }
        }
    }
}
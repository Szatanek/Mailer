using System;
using System.Data.SqlClient;
using Dapper;
using Framework.Tests.Infrastructure;
using Framework.Utils;

namespace Mailer.Services.Tests.Infrastructure
{
    internal sealed class MailerDataBuilder : BaseDataBuilder
    {
        private readonly string connectionString;

        public MailerDataBuilder(TestSettings testSettings)
        {
            connectionString = testSettings.ConnectionString;
            Cleanup();
        }

        internal MailerDataBuilder AddMail(Guid mailGuid, string recipient, string sender, string topic, string content, byte status, int systemId, bool isHtml = false)
        {
            const string sql = @"INSERT INTO [mailer].[Mails] (MailGuid, Topic, Sender, Recipient, Content, Status, IsHtml, SystemId, Timestamp)
                                VALUES (@mailGuid, @topic, @sender, @recipient, @content, @status, @isHtml, @systemId, @timestamp)";
            var parameters = new
            {
                mailGuid,
                recipient,
                sender,
                topic,
                content,
                status,
                isHtml,
                systemId,
                timestamp = DateTimeProvider.Current.Now,
            };

            Enqueue(sql, parameters);
            return this;
        }

        internal MailerDataBuilder AddMailResponse(Guid mailGuid, string errorMessage, string errorType)
        {
            const string sql = @"DECLARE @mailId int;
                                SELECT @mailId = Id FROM [mailer].[Mails] WHERE MailGuid = @mailGuid

                                INSERT INTO [mailer].[MailResponses] (MailId, ErrorMessage, ErrorType, Timestamp)
                                VALUES (@mailId, @errorMessage, @errorType, @timestamp)";
            var parameters = new
            {
                mailGuid,
                errorMessage,
                errorType,
                timestamp = DateTimeProvider.Current.Now,
            };

            Enqueue(sql, parameters);
            return this;
        }

        internal MailerDataBuilder AddUser(string login, string firstName, string lastName)
        {
            const string sql = @"INSERT INTO [mailer].[Users] (Login, FirstName, LastName)
                                VALUES (@login, @firstName, @lastName)";
            var parameters = new
            {
                login,
                firstName,
                lastName,
                timestamp = DateTimeProvider.Current.Now,
            };

            Enqueue(sql, parameters);
            return this;
        }

        protected override void Execute(string sql, object parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }

        private MailerDataBuilder Cleanup()
        {
            Enqueue("DELETE FROM [mailer].[MailResponses]");
            Enqueue("DELETE FROM [mailer].[Mails]");
            return this;
        }
    }
}
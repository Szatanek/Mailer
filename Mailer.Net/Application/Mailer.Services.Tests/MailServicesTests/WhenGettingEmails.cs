using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Framework.Application;
using Mailer.Domain.Mail;
using Mailer.Services.Contracts.Read.Queries;
using Mailer.Services.Contracts.Read.Views;
using Xunit;

namespace Mailer.Services.Tests.MailServicesTests
{
    public sealed class WhenGettingEmails : BaseIntegrationTest
    {
        private const int SystemId = 5;
        private readonly IApplicationQueryDispatcher queryDispatcher;

        public WhenGettingEmails()
        {
            queryDispatcher = GetService<IApplicationQueryDispatcher>();
        }

        [Fact]
        public async Task ShouldAddMailToDatabase()
        {
            // Given
            const int expectedEmailsCount = 2;
            const string expectedEmailStatus = "New";
            var query = new GetMailsQuery(SystemId);

            // When
            var results = await queryDispatcher.DispatchAsync<GetMailsQuery, IEnumerable<MailReadViewModel>>(query, CancellationToken.None);

            // Then
            results.Should().NotBeNullOrEmpty();
            results.Should().HaveCount(expectedEmailsCount);
            results.Should().Contain(m => m.Status == expectedEmailStatus);
        }

        protected override void Seed()
        {
            Builder
                .AddMail(Guid.NewGuid(), "to@test.com", "from@test.com", "Test", "Test content", (byte)MailStatus.New, SystemId)
                .AddMail(Guid.NewGuid(), "to@test.com", "from@test.com", "Test", "Test content", (byte)MailStatus.Sent, SystemId)
                .AddMail(Guid.NewGuid(), "to@test.com", "from@test.com", "New System mail", "Test content", (byte)MailStatus.Sent, 2)
                .Build();
        }
    }
}

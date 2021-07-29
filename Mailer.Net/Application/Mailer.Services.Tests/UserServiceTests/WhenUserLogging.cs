using System;
using FluentAssertions;
using Mailer.Domain.User;
using Mailer.Services.Contracts.Read.Queries;
using Xunit;

namespace Mailer.Services.Tests.UserServiceTests
{
    public sealed class WhenGettingUserDetails : BaseIntegrationTest
    {
        private const string ExistingLogin = "Admin";

        private UserService userService;

        public WhenGettingUserDetails()
        {
            userService = GetService<UserService>();
        }


        [Fact]
        public void ShouldReturnUserDetails()
        {
            // Given
            const string expectedUserName = "Admin Istrator";
            var query = new GetUserDetailsQuery
            {
                Login = ExistingLogin,
            };

            // When
            var details = userService.GetUserDetails(query);

            // Then
            details.Name.Should().Be(expectedUserName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenUserNotFound()
        {
            // Given
            const string invalidLogin = "admin1";
            const string invalidPin = "4557";
            var query = new GetUserDetailsQuery
            {
                Login = invalidLogin,
                Pin = invalidPin
            };

            // When
            Action getUser = () => userService.GetUserDetails(query);

            // Then
            getUser.Should().Throw<UserNotFoundException>();
        }

        protected override void Seed()
        {
            Builder
                .AddUser(ExistingLogin, "Admin", "Istrator")
                .Build();
        }
    }
}

using System;
using FluentAssertions;
using Mailer.Domain.Mail;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenCreatingMailTopic
    {
        [Fact]
        public void ShouldCreateNewMailTopic()
        {
            // Arrange
            const string value = "This if mail topic";

            // Act
            var mailTopic = (MailTopic)value;

            // Assert
            mailTopic.Should().NotBeNull();
            ((string)mailTopic).Should().Be(value);
        }

        [Fact]
        public void ShouldThrowExceptionForEmptyTopic()
        {
            // Arrange
            var invalidTopic = string.Empty;

            // Act
            Action createTopic = () =>
            { 
                var topic = (MailTopic)invalidTopic; 
            };

            // Assert
            createTopic.Should().Throw<InvalidMailTopicException>();
        }

        [Fact]
        public void ShouldThrowExceptionForTooLongTopic()
        {
            // Arrange
            var invalidTopic = new string('a', 251);

            // Act
            Action createTopic = () =>
            {
                var topic = (MailTopic)invalidTopic;
            };

            // Assert
            createTopic.Should().Throw<InvalidMailTopicException>();
        }
    }
}

using FluentAssertions;
using Mailer.Domain.System;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenReconstitutingSystem : BaseUnitTest
    {
        [Fact]
        public void ShouldCreateNewSystemWithAllParameters()
        {
            // Arrange
            var systemId = (SystemId)5;
            var name = "etiSZOP";

            // Act
            var system = SystemEntity.Reconstitute(systemId, name);

            // Assert
            system.Should().NotBeNull();
            system.Id.Should().Be(systemId);
            system.Name.Should().Be(name);
        }

        [Fact]
        public void ShouldNotCreateEvent()
        {
            // Arrange
            var systemId = (SystemId)5;
            var name = "etiSZOP";

            // Act
            var system = SystemEntity.Reconstitute(systemId, name);

            // Assert
            system.Should().NotBeNull();
            system.Events.Should().NotBeNull();
            system.Events.Should().BeEmpty();
        }
    }
}

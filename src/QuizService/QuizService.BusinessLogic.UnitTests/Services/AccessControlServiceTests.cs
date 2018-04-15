using Moq;
using QuizService.BusinessLogic.Services;
using QuizService.Interfaces.Services;
using QuizService.Model;
using QuizService.Model.Interfaces;
using Xunit;

namespace QuizService.BusinessLogic.UnitTests.Services
{
    public class AccessControlServiceTests
    {
        [Fact]
        public void HasAccess_WhenCurrentUserMatchesResourceOwnerForUserOwnedResource_ReturnsTrue()
        {
            // Arrange
            var currentUser = new User()
            {
                Id = 123
            };
            var userAccessor = new Mock<IUserAccessorService>();
            userAccessor.SetupGet(ua => ua.DomainUser).Returns(currentUser);

            var service = new AccessControlService(userAccessor.Object);
            var resourceMock = new Mock<IUserOwnedResource>();
            resourceMock.SetupGet(r => r.CreatedUserId).Returns(123);

            // Act
            var hasAccess = service.HasAccess(resourceMock.Object);

            // Assert
            Assert.True(hasAccess);
        }

        [Fact]
        public void HasAccess_WhenUserIsAdminForUserOwnedResource_ReturnsTrue()
        {
            // Arrange
            var currentUser = new User()
            {
                Id = 555
            };
            currentUser.Roles.Add(ApplicationRole.Admin);
            var userAccessor = new Mock<IUserAccessorService>();
            userAccessor.SetupGet(ua => ua.DomainUser).Returns(currentUser);

            var service = new AccessControlService(userAccessor.Object);
            var resourceMock = new Mock<IUserOwnedResource>();
            resourceMock.SetupGet(r => r.CreatedUserId).Returns(123);

            // Act
            var hasAccess = service.HasAccess(resourceMock.Object);

            // Assert
            Assert.True(hasAccess);
        }

        [Fact]
        public void HasAccess_WhenUserDoesNotMatchResourceOwnerForUserOwnedResource_ReturnsFalse()
        {
            // Arrange
            var currentUser = new User()
            {
                Id = 555
            };
            var userAccessor = new Mock<IUserAccessorService>();
            userAccessor.SetupGet(ua => ua.DomainUser).Returns(currentUser);

            var service = new AccessControlService(userAccessor.Object);
            var resourceMock = new Mock<IUserOwnedResource>();
            resourceMock.SetupGet(r => r.CreatedUserId).Returns(123);

            // Act
            var hasAccess = service.HasAccess(resourceMock.Object);

            // Assert
            Assert.False(hasAccess);
        }
    }
}

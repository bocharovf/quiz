using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QuizService.Auth;
using QuizService.Controllers;
using QuizService.DataAccess.Auth;
using QuizService.Interfaces.Services;
using QuizService.Model;
using QuizService.Model.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizService.UnitTests
{
    public class AuthControllerTests
    {
        #region Register

        private readonly RegistrationContract ValidRegistrationContract = new RegistrationContract()
        {
            DisplayName = "User",
            Email = "user@domain.com",
            Password = "password"
        };

        [Fact]
        public async void Register_WhenRegistrationSucceeded_ReturnsOkWithSucceedOperationResult()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            mockAuthService.Setup(manager => manager.CreateAsync(It.IsAny<AspnetUser>(), It.IsAny<string>()))
                           .Returns(value: Task.FromResult(IdentityResult.Success));
            mockAuthService.Setup(manager => manager.AddToRoleAsync(It.IsAny<AspnetUser>(), It.IsAny<string>()))
                           .Returns(value: Task.FromResult(IdentityResult.Success));
            mockAuthService.Setup(manager => manager.SignInAsync(It.IsAny<AspnetUser>(), It.IsAny<bool>()))
                           .Returns(value: Task.FromResult(IdentityResult.Success));

            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = await controller.Register(ValidRegistrationContract);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var operationResult = Assert.IsAssignableFrom<OperationResultContract>(objectResult.Value);
            Assert.NotNull(operationResult);
            Assert.True(operationResult.IsSuccessful);
            Assert.Empty(operationResult.Errors);
        }

        [Fact]
        public async void Register_Always_CorrectlyConstructsUser()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            mockAuthService.Setup(manager => manager.CreateAsync(It.IsAny<AspnetUser>(), It.IsAny<string>()))
                           .Returns(value: Task.FromResult(IdentityResult.Success));
            mockAuthService.Setup(manager => manager.AddToRoleAsync(It.IsAny<AspnetUser>(), It.IsAny<string>()))
                           .Returns(value: Task.FromResult(IdentityResult.Success));
            mockAuthService.Setup(manager => manager.SignInAsync(It.IsAny<AspnetUser>(), It.IsAny<bool>()))
                           .Returns(value: Task.FromResult(IdentityResult.Success));

            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = await controller.Register(ValidRegistrationContract);

            // Assert
            mockAuthService.Verify(service => service.CreateAsync(
                It.Is<AspnetUser>(u =>
                    u.Email == ValidRegistrationContract.Email &&
                    u.UserName == ValidRegistrationContract.DisplayName
                ), 
                ValidRegistrationContract.Password), 
            Times.Once);
        }

        [Fact]
        public async void Register_WhenUserCreationFailed_ReturnsBadRequestWithFailedOperationResult()
        {
            // Arrange
            var creationErrors = new IdentityError[] {
                new IdentityError() {
                    Code = "ERROR_CODE1",
                    Description = "Unable to create user 1."
                },
                new IdentityError() {
                    Code = "ERROR_CODE2",
                    Description = "Unable to create user 2."
                }
            };

            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            mockAuthService.Setup(manager => manager.CreateAsync(It.IsAny<AspnetUser>(), It.IsAny<string>()))
                           .Returns(value: Task.FromResult(IdentityResult.Failed(creationErrors)));

            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = await controller.Register(ValidRegistrationContract);

            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(response);
            var operationResult = Assert.IsAssignableFrom<OperationResultContract>(objectResult.Value);
            Assert.NotNull(operationResult);
            Assert.False(operationResult.IsSuccessful);
            Assert.NotEmpty(operationResult.Errors);
            Assert.Equal(2, operationResult.Errors.Count());
            Assert.Equal("ERROR_CODE1", operationResult.Errors.ToArray()[0].Code);
            Assert.Equal("ERROR_CODE2", operationResult.Errors.ToArray()[1].Code);
        }

        [Fact]
        public async void Register_WhenAddingToRoleFailed_ThrowsAuthenticationError()
        {
            // Arrange
            var addToRole = new IdentityError[] {
                new IdentityError() {
                    Code = "ERROR_CODE",
                    Description = "Unable to create user."
                }
            };

            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            mockAuthService.Setup(manager => manager.CreateAsync(It.IsAny<AspnetUser>(), It.IsAny<string>()))
                           .Returns(value: Task.FromResult(IdentityResult.Success));
            mockAuthService.Setup(manager => manager.AddToRoleAsync(It.IsAny<AspnetUser>(), It.IsAny<string>()))
                           .Returns(value: Task.FromResult(IdentityResult.Failed(addToRole)));

            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var exception = await Assert.ThrowsAsync<AuthenticationException>(
                async () => await controller.Register(ValidRegistrationContract));
            
            // Assert
            var identityError = Assert.Single<IdentityError>(exception.IdentityErrors);
            Assert.Equal("ERROR_CODE", identityError.Code);
        }

        [Fact]
        public async void Register_WhenNullRegistrationContract_ThrowsArgumentNull()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>("registrationContract", 
                async () => await controller.Register(null));
        }

        #endregion

        #region Login

        private readonly LoginContract ValidLoginContract = new LoginContract() {
            Login = "user",
            Password = "password",
            Remember = true
        };

        [Fact]
        public async void Login_WhenLoginSucceeded_ReturnsOkWithSucceedOperationResult()
        {
            // Arrange
            var signInResult = Microsoft.AspNetCore.Identity.SignInResult.Success;

            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            mockAuthService.Setup(manager => manager.PasswordSignInAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(value: Task.FromResult(signInResult));

            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = await controller.Login(ValidLoginContract);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var operationResult = Assert.IsAssignableFrom<OperationResultContract>(objectResult.Value);
            Assert.NotNull(operationResult);
            Assert.True(operationResult.IsSuccessful);
            Assert.Empty(operationResult.Errors);
        }

        [Fact]
        public async void Login_WhenSignInFailed_ReturnsBadRequestWithFailedOperationResult()
        {
            // Arrange
            var signInResult = Microsoft.AspNetCore.Identity.SignInResult.Failed;

            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            mockAuthService.Setup(manager => manager.PasswordSignInAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(value: Task.FromResult(signInResult));

            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = await controller.Login(ValidLoginContract);

            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(response);
            var operationResult = Assert.IsAssignableFrom<OperationResultContract>(objectResult.Value);
            Assert.NotNull(operationResult);
            Assert.False(operationResult.IsSuccessful);
            Assert.Single(operationResult.Errors);
        }

        [Fact]
        public async void Login_WhenNullLoginContract_ThrowsArgumentNull()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>("loginContract",
                async () => await controller.Login(null));
        }

        #endregion

        #region Logout

        [Fact]
        public async void Logout_Always_ReturnsOk()
        {
            // Arrange
            var signInResult = Microsoft.AspNetCore.Identity.SignInResult.Success;

            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            mockAuthService.Setup(manager => manager.SignOutAsync())
                .Returns(value: Task.CompletedTask);

            var mockUserAccessor = new Mock<IUserAccessorService>();

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = await controller.Logout();

            // Assert
            var objectResult = Assert.IsType<OkResult>(response);
        }

        #endregion

        #region Status

        [Fact]
        public void Status_WhenUserAuthenticated_ReturnsOkWithAuthenticatedAuthenticationStatus()
        {
            // Arrange
            var user = new User()
            {
                Id = 1,
                Name = "User1",
                Email = "user1@domain.com",
                Roles = new List<string>() { "User" }
            };

            var mockAuthService = new Mock<IAuthenticationWrapperService>();

            var mockUserAccessor = new Mock<IUserAccessorService>();
            mockUserAccessor.SetupGet(ua => ua.IsAuthenticated).Returns(value: true);
            mockUserAccessor.SetupGet(ua => ua.DomainUser).Returns(value: user);

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = controller.Status();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var authenticationStatus = Assert.IsAssignableFrom<AuthenticationStatusContract>(objectResult.Value);
            Assert.NotNull(authenticationStatus);
            Assert.True(authenticationStatus.IsSignedIn);
            Assert.NotNull(authenticationStatus.User);
            Assert.Equal(user, authenticationStatus.User);
        }

        [Fact]
        public void Status_WhenUserNotAuthenticated_ReturnsOkWithNotAuthenticatedAuthenticationStatus()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthenticationWrapperService>();
            var mockUserAccessor = new Mock<IUserAccessorService>();
            mockUserAccessor.SetupGet(ua => ua.IsAuthenticated).Returns(value: false);

            var controller = new AuthController(mockAuthService.Object, mockUserAccessor.Object);

            // Act
            var response = controller.Status();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var authenticationStatus = Assert.IsAssignableFrom<AuthenticationStatusContract>(objectResult.Value);
            Assert.NotNull(authenticationStatus);
            Assert.False(authenticationStatus.IsSignedIn);
            Assert.Null(authenticationStatus.User);
        }

        #endregion
    }
}

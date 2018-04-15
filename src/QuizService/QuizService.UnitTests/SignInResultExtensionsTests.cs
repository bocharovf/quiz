using Microsoft.AspNetCore.Identity;
using QuizService.Auth;
using QuizService.Model.DataContract;
using Xunit;

namespace QuizService.UnitTests
{
    public class SignInResultExtensionsTests
    {
        [Fact]
        public void ToOperationResultContract_WhenSignInSucceeded_ReturnsSucceededContract() {
            // Arrange & Act
            var contract = SignInResultExtensions.ToOperationResultContract(SignInResult.Success);

            // Assert
            Assert.NotNull(contract);
            Assert.True(contract.IsSuccessful);
            Assert.Empty(contract.Errors);
        }

        [Fact]
        public void ToOperationResultContract_WhenUserLockedOut_ReturnsContractWithIsLockedOutCode()
        {
            // Arrange & Act
            var contract = SignInResultExtensions.ToOperationResultContract(SignInResult.LockedOut);

            // Assert
            Assert.NotNull(contract);
            Assert.False(contract.IsSuccessful);
            var error = Assert.Single(contract.Errors);
            Assert.Equal(SignInErrorCode.IsLockedOut.ToString(), error.Code);
        }

        [Fact]
        public void ToOperationResultContract_WhenSignInNotAllowed_ReturnsContractWithNotAllowedCode()
        {
            // Arrange & Act
            var contract = SignInResultExtensions.ToOperationResultContract(SignInResult.NotAllowed);

            // Assert
            Assert.NotNull(contract);
            Assert.False(contract.IsSuccessful);
            var error = Assert.Single(contract.Errors);
            Assert.Equal(SignInErrorCode.IsNotAllowed.ToString(), error.Code);
        }

        [Fact]
        public void ToOperationResultContract_WhenRequiresTwoFactor_ReturnsContractWithRequiresTwoFactorCode()
        {
            // Arrange & Act
            var contract = SignInResultExtensions.ToOperationResultContract(SignInResult.TwoFactorRequired);

            // Assert
            Assert.NotNull(contract);
            Assert.False(contract.IsSuccessful);
            var error = Assert.Single(contract.Errors);
            Assert.Equal(SignInErrorCode.RequiresTwoFactor.ToString(), error.Code);
        }

        [Fact]
        public void ToOperationResultContract_WhenSignInFailed_ReturnsContractWithInvalidCredentialsCode()
        {
            // Arrange & Act
            var contract = SignInResultExtensions.ToOperationResultContract(SignInResult.Failed);

            // Assert
            Assert.NotNull(contract);
            Assert.False(contract.IsSuccessful);
            var error = Assert.Single(contract.Errors);
            Assert.Equal(SignInErrorCode.InvalidCredentials.ToString(), error.Code);
        }
    }
}

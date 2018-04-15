using Microsoft.AspNetCore.Identity;
using QuizService.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace QuizService.UnitTests
{
    public class IdentityResultExtensionsTests
    {
        [Fact]
        public void ToOperationResultContract_WhenSucceeded_ReturnsSucceededContract()
        {
            // Arrange & Act
            var contract = IdentityResultExtensions.ToOperationResultContract(IdentityResult.Success);

            // Assert
            Assert.NotNull(contract);
            Assert.True(contract.IsSuccessful);
            Assert.Empty(contract.Errors);
        }

        [Fact]
        public void ToOperationResultContract_WhenFailed_ReturnsFailedContract()
        {
            // Arrange
            var result = IdentityResult.Failed(
                    new IdentityError()
                    {
                        Code = "ERROR_CODE1",
                        Description = "Description1"
                    },
                    new IdentityError()
                    {
                        Code = "ERROR_CODE2",
                        Description = "Description2"
                    }
                );

            // Act
            var contract = IdentityResultExtensions.ToOperationResultContract(result);

            // Assert
            Assert.NotNull(contract);
            Assert.False(contract.IsSuccessful);
            Assert.Equal(2, contract.Errors.Count());
            Assert.Equal("ERROR_CODE1", contract.Errors.ToArray()[0].Code);
            Assert.Equal("ERROR_CODE2", contract.Errors.ToArray()[1].Code);
        }
    }
}

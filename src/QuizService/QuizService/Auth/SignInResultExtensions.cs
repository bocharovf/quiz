using Microsoft.AspNetCore.Identity;
using QuizService.BusinessLogic;
using QuizService.Model.DataContract;
using System.Collections.Generic;

namespace QuizService.Auth
{
    /// <summary>
    /// Provides extension methods for <see cref="SignInResult"/>.
    /// </summary>
    public static class SignInResultExtensions
    {
        /// <summary>
        /// Converts <see cref="SignInResult"/> to <see cref="OperationResultContract"/>.
        /// </summary>
        /// <param name="signInResult">The signin result.</param>
        /// <returns>Operation result contract.</returns>
        public static OperationResultContract ToOperationResultContract(this SignInResult signInResult)
        {
            ThrowIf.Null(signInResult, nameof(signInResult));

            List<LocalizableErrorContract> errors = new List<LocalizableErrorContract>();

            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut)
                {
                    errors.Add(new LocalizableErrorContract(SignInErrorCode.IsLockedOut.ToString(), "User is locked out."));
                }
                else if (signInResult.IsNotAllowed)
                {
                    errors.Add(new LocalizableErrorContract(SignInErrorCode.IsNotAllowed.ToString(), "Sign in disabled for user."));
                }
                else if (signInResult.RequiresTwoFactor)
                {
                    errors.Add(new LocalizableErrorContract(SignInErrorCode.RequiresTwoFactor.ToString(), "User must use two factor authentication."));
                }
                else
                {
                    errors.Add(new LocalizableErrorContract(SignInErrorCode.InvalidCredentials.ToString(), "Invalid user name or password."));
                }
            }

            return new OperationResultContract(errors);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using QuizService.BusinessLogic;
using QuizService.Model.DataContract;
using System.Linq;

namespace QuizService.Auth
{
    /// <summary>
    /// Provides extension methods for <see cref="IdentityResult"/>.
    /// </summary>
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// Converts <see cref="IdentityResult"/> to <see cref="OperationResultContract"/>.
        /// </summary>
        /// <param name="identityResult">The identity result.</param>
        /// <returns>Operation result contract.</returns>
        public static OperationResultContract ToOperationResultContract(this IdentityResult identityResult)
        {
            ThrowIf.Null(identityResult, nameof(identityResult));

            var errors = identityResult.Errors.Select(e => new LocalizableErrorContract(e.Code, e.Description));
            return new OperationResultContract(errors);
        }
    }
}

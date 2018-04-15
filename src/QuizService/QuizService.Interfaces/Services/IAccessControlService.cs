using QuizService.Model.Interfaces;
using System;
using System.Linq.Expressions;

namespace QuizService.Interfaces.Services
{
    /// <summary>
    /// Provides methods to check access to resources.
    /// </summary>
    public interface IAccessControlService
    {
        /// <summary>
        /// Checks whether user has access to the resource.
        /// </summary>
        /// <param name="resource">The resource to check.</param>
        /// <returns>True if access is granted; False otherwise.</returns>
        bool HasAccess(IUserOwnedResource resource);

        /// <summary>
        /// Gets filtering access expression.
        /// </summary>
        /// <typeparam name="T">Entity to filter.</typeparam>
        /// <returns>Filter expression.</returns>
        Expression<Func<T, bool>> GetAccessExpression<T>() where T : IUserOwnedResource;
    }
}
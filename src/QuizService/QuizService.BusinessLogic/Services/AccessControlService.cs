using QuizService.Interfaces.Services;
using QuizService.Model;
using QuizService.Model.Interfaces;
using System;
using System.Linq.Expressions;

namespace QuizService.BusinessLogic.Services
{
    /// <summary>
    /// Provides methods to check access to resources.
    /// </summary>
    public class AccessControlService : IAccessControlService
    {
        private readonly IUserAccessorService UserAccessor;
        private readonly int AnonymousUserId = User.Anonymous.Id;
        private readonly User CurrentUser;

        public AccessControlService(IUserAccessorService userAccessor)
        {
            this.UserAccessor = userAccessor;
            this.CurrentUser = userAccessor.DomainUser;
        }

        /// <summary>
        /// Checks whether user has access to the resource.
        /// </summary>
        /// <param name="resource">The resource to check.</param>
        /// <returns>True if access is granted; False otherwise.</returns>
        public bool HasAccess(IUserOwnedResource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            Func<IUserOwnedResource, bool> accessFunction = this.GetAccessExpression<IUserOwnedResource>().Compile();
            return accessFunction(resource);
        }

        /// <summary>
        /// Gets filtering access expression.
        /// </summary>
        /// <typeparam name="T">Entity to filter.</typeparam>
        /// <returns>Filter expression.</returns>
        public Expression<Func<T, bool>> GetAccessExpression<T>() where T: IUserOwnedResource
        {
            if (this.CurrentUser.IsAdmin)
            {
                return (T resource) => true;
            }

            return (T resource) =>
                resource.CreatedUserId == AnonymousUserId ||
                resource.CreatedUserId == this.CurrentUser.Id;
        }
    }
}

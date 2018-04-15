using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizService.Auth
{
    /// <summary>
    /// Represents unexpected authentication exception.
    /// </summary>
    public class AuthenticationException: Exception
    {
        /// <summary>
        /// Gets collection of identity errors.
        /// </summary>
        public IEnumerable<IdentityError> IdentityErrors { get; private set; }

        public AuthenticationException(string message, IEnumerable<IdentityError> identityErrors): 
            base(FormatMessage(message, identityErrors))
        {
            this.IdentityErrors = identityErrors ?? new List<IdentityError>();
        }

        private static string FormatMessage(string message, IEnumerable<IdentityError> identityErrors)
        {
            string errorList = String.Join(", ", identityErrors.Select(e => $"({e.Code} - {e.Description})"));
            return $"{message}; Internal errors: {errorList}";
        }
    }
}

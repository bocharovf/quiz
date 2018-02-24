using Microsoft.AspNetCore.Http;

namespace QuizService.Extensions
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Checks whether <see cref="HttpResponse"/> has error status code.
        /// </summary>
        /// <param name="response">Http response.</param>
        /// <returns>Value identifying whether <see cref="HttpResponse"/> has error status code.</returns>
        public static bool HasError(this HttpResponse response) => response.StatusCode >= 400;
    }
}

using Microsoft.AspNetCore.Http;

namespace QuizService.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Gets requested resource Url.
        /// </summary>
        /// <param name="request">Http request.</param>
        /// <returns>Requested resource Url</returns>
        public static string GetRequestUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        }
    }
}

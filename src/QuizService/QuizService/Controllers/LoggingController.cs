using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizService.BusinessLogic;
using QuizService.Common.Logging;
using QuizService.Extensions;
using QuizService.Filters;
using QuizService.Model.DataContract;

namespace QuizService.Controllers
{
    [QuizExceptionFilter]
    [Produces("application/json")]
    [Route("api/log")]
    public class LoggingController : Controller
    {
        private readonly ILogger Logger;
        private readonly ILogFormatter LogFormatter;

        public LoggingController(ILogger<LoggingController> logger, ILogFormatter logFormatter)
        {
            this.Logger = logger;
            this.LogFormatter = logFormatter;
        }

        // POST api/log/error
        [HttpPost]
        [Route("error")]
        public void Log([FromBody] ClientExceptionContract clientException)
        {
            ThrowIf.Null(clientException, nameof(clientException));

            if (string.IsNullOrWhiteSpace(clientException.CorrelationId))
            {
                clientException.CorrelationId = HttpContext.GetCorrelationId();
            }

            string message = this.LogFormatter.FormatClientException(clientException);
            this.Logger.LogError(ApplicationLogEvents.ClientError, message);
        }
    }
}
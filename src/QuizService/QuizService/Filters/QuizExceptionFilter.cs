using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizService.BusinessLogic.Exceptions;
using QuizService.Extensions;
using QuizService.Model.DataContract;
using System;

namespace QuizService.Filters
{
    public class QuizExceptionFilter : ExceptionFilterAttribute
    {
        private const string DEFAULT_ERROR_MESSAGE = "Unexpected server error.";
        private const string DEFAULT_ERROR_CODE = "InternalServerError";

        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            string correlationId = context.HttpContext.GetCorrelationId();

            IActionResult result;
            ExceptionContract errorData;

            if (ex is BusinessLogicException businessLogicException)
            {
                errorData = new ExceptionContract(businessLogicException, correlationId);
                if (ex is EntityNotFoundException)
                {
                    result = new NotFoundObjectResult(errorData);
                }
                else
                {
                    result = new BadRequestObjectResult(errorData);
                }
            }
            else
            {
                errorData = new ExceptionContract(DEFAULT_ERROR_MESSAGE, DEFAULT_ERROR_CODE, correlationId);
                result = new ObjectResult(errorData)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.Result = result;
        }
    }
}

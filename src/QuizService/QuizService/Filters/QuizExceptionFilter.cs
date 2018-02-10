using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizService.BusinessLogic.Exceptions;
using QuizService.Model.DataContract;
using System;

namespace QuizService.Filters
{
    public class QuizExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            
            IActionResult result = new ObjectResult(new DefaultServiceExceptionContract())
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            if (ex is BusinessLogicException businessLogicException)
            {
                var errorData = new ServiceExceptionContract(businessLogicException);
                if (ex is EntityNotFoundException)
                {
                    result = new NotFoundObjectResult(errorData);
                }
                else
                {
                    result = new BadRequestObjectResult(errorData);
                }
            }

            context.Result = result;
        }
    }
}

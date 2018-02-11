using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using QuizService.BusinessLogic.Exceptions;
using QuizService.Filters;
using QuizService.Model;
using QuizService.Model.DataContract;
using System;
using System.Collections.Generic;
using Xunit;

namespace QuizService.UnitTests
{
    public class QuizExceptionFilterTests
    {
        [Fact]
        public void OnException_WhenEntityNotFoundException_Expects404NotFound()
        {
            // Arrange
            var filter = new QuizExceptionFilter();
            var ex = new EntityNotFoundException(typeof(Quiz), 1);
            var exceptionContext = this.MockExceptionContext(ex);

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var result = Assert.IsType<NotFoundObjectResult>(exceptionContext.Result);
            Assert.Equal(404, result.StatusCode);
            var contract = Assert.IsType<ServiceExceptionContract>(result.Value);
            Assert.Equal("EntityNotFound", contract.ErrorCode);
        }

        [Fact]
        public void OnException_WhenBusinessLogicException_Expects400BadRequest()
        {
            // Arrange
            var filter = new QuizExceptionFilter();
            var ex = new BusinessLogicException("CustomBusinessLogicException");
            var exceptionContext = this.MockExceptionContext(ex);

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(exceptionContext.Result);
            Assert.Equal(400, result.StatusCode);
            var contract = Assert.IsType<ServiceExceptionContract>(result.Value);
            Assert.Equal("CustomBusinessLogicException", contract.ErrorCode);
        }

        [Fact]
        public void OnException_WhenNotABusinessLogicException_Expects500InternalServerError()
        {
            // Arrange
            var filter = new QuizExceptionFilter();
            var ex = new NullReferenceException("Custom exception");
            var exceptionContext = this.MockExceptionContext(ex);

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var result = Assert.IsType<ObjectResult>(exceptionContext.Result);
            Assert.Equal(500, result.StatusCode);
            var contract = Assert.IsType<DefaultServiceExceptionContract>(result.Value);
            Assert.Equal("InternalServerError", contract.ErrorCode);
        }

        private ExceptionContext MockExceptionContext(Exception ex)
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var context = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = ex
            };

            return context;
        }
    }
}

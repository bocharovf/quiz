using System;
using System.Collections.Generic;
using System.Text;

namespace QuizService.Model.DataContract
{
    public class DefaultServiceExceptionContract : IServiceExceptionContract
    {
        public string ErrorCode => "InternalServerError";

        public object Extension => null;

        public string Message => "Unexpected server error.";
    }
}

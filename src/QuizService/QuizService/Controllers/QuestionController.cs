using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Managers;
using QuizService.Model;

namespace QuizService.Controllers
{
    [Produces("application/json")]
    [Route("api/question")]
    public class QuestionController : Controller
    {
        private IUnitOfWork Uow;
        private IQuizFlowManager QuizFlowManager;

        public QuestionController(IQuizFlowManager quizFlowManager, IUnitOfWork uow)
        {
            this.QuizFlowManager = quizFlowManager;
            this.Uow = uow;
        }

    }
}
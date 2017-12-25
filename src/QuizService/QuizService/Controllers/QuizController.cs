using Microsoft.AspNetCore.Mvc;
using QuizService.Model.DataContract;
using QuizService.Filters;
using QuizService.Interfaces.Managers;
using QuizService.Model;

namespace QuizService.Controllers
{
    [QuizExceptionFilter]
    [Produces("application/json")]
    [Route("api/quizzes")]
    public class QuizController : Controller
    {
        private IQuizFlowManager QuizFlowManager;

        public QuizController(IQuizFlowManager quizFlowManager)
        {
            this.QuizFlowManager = quizFlowManager;
        }

        // POST api/quizzes?templateId={templateId}
        [HttpPost]
        public IActionResult Start([FromQuery] int templateId)
        {
            Quiz quiz = this.QuizFlowManager.StartNewQuiz(templateId);
            return Ok(quiz);
        }

        // POST api/quizzes/{quizId}/nextquestion
        [HttpPost("{quizId}/nextquestion")]
        public IActionResult GetNextQuestion(int quizId)
        {
            QuizFlowCommand command = this.QuizFlowManager.GetNextQuestion(quizId);         
            return Ok(command);
        }
    }
}
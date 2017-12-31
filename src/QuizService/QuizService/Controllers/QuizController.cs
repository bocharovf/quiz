using Microsoft.AspNetCore.Mvc;
using QuizService.BusinessLogic;
using QuizService.Filters;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Managers;
using QuizService.Model;
using QuizService.Model.DataContract;

namespace QuizService.Controllers
{
    [QuizExceptionFilter]
    [Produces("application/json")]
    [Route("api/quizzes")]
    public class QuizController : Controller
    {
        private IUnitOfWork Uow;
        private IQuizFlowManager QuizFlowManager;

        public QuizController(IQuizFlowManager quizFlowManager, IUnitOfWork uow)
        {
            this.QuizFlowManager = quizFlowManager;
            this.Uow = uow;
        }

        // POST api/quizzes?templateId={templateId}
        [HttpPost]
        public IActionResult Start([FromQuery] int templateId)
        {
            QuizTemplate quizTemplate = this.Uow.QuizTemplateRepository.GetByID(templateId);
            ThrowIf.NotFound(quizTemplate, templateId);

            Quiz quiz = this.QuizFlowManager.StartNewQuiz(quizTemplate);
            return Ok(quiz);
        }

        // POST api/quizzes/{quizId}/nextquestion
        [HttpPost("{quizId}/nextquestion")]
        public IActionResult GetNextQuestion(int quizId)
        {
            Quiz quiz = this.Uow.QuizRepository.GetByID(quizId);
            ThrowIf.NotFound(quiz, quizId);

            QuizFlowCommandContract command = this.QuizFlowManager.GetNextQuestion(quiz);         
            return Ok(command);
        }

        // PATCH api/quizzes/{quizId}/questions/{questionId}?answerTemplateId={answerTemplateId}
        [HttpPatch("{quizId}/questions/{questionId}")]
        public IActionResult AnswerQuestion(int quizId, int questionId, int answerTemplateId)
        {
            Quiz quiz = this.Uow.QuizRepository.GetByID(quizId);
            ThrowIf.NotFound(quiz, quizId);

            Question question = quiz.GetQuestion(questionId);
            ThrowIf.NotFound(question, questionId);

            this.QuizFlowManager.AnswerQuestion(question, answerTemplateId);
            return Ok();
        }

        // POST api/quizzes/{quizId}/complete
        [HttpPatch("{quizId}/complete")]
        public IActionResult CompleteQuiz(int quizId)
        {
            Quiz quiz = this.Uow.QuizRepository.GetByID(quizId);
            ThrowIf.NotFound(quiz, quizId);

            this.QuizFlowManager.CompleteQuiz(quiz);
            return Ok();
        }
    }
}
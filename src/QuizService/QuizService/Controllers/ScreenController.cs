using Microsoft.AspNetCore.Mvc;
using QuizService.BusinessLogic;
using QuizService.Filters;
using QuizService.Interfaces.Common;
using QuizService.Model.DataContract;

namespace QuizService.Controllers
{
    [QuizExceptionFilter]
    [Produces("application/json")]
    [Route("api")]
    public class ScreenController : Controller
    {
        private IUnitOfWork Uow;

        public ScreenController(IUnitOfWork uow)
        {
            this.Uow = uow;
        }

        [HttpGet("quiz-templates")]
        public IActionResult GetQuizTemplates()
        {
            return Ok(this.Uow.QuizTemplateRepository.Get());
        }

        [HttpGet("quiz-templates/{quizTemplateId}")]
        public IActionResult GetQuizTemplate(int quizTemplateId)
        {
            var quizTemplate = this.Uow.QuizTemplateRepository.GetByID(quizTemplateId);
            ThrowIf.NotFound(quizTemplate, quizTemplateId);

            var questionTemplateCount = this.Uow.QuizTemplateRepository
                                                .GetQuestionTemplateCount(quizTemplateId);

            var quizTemplateDetails = new QuizTemplateDetailsContract(quizTemplate, questionTemplateCount);
            return Ok(quizTemplateDetails);
        }

        [HttpGet("scores")]
        public IActionResult GetScores()
        {
            return Ok(this.Uow.ScoreRepository.Get());
        }

        [HttpGet("scores/{scoreId}")]
        public IActionResult GetScore(int scoreId)
        {
            var score = this.Uow.ScoreRepository.GetByID(scoreId);
            ThrowIf.NotFound(score, scoreId);
            return Ok(score);
        }

        [HttpGet("quizzes/{quizId}/scores")]
        public IActionResult GetQuizScore(int quizId)
        {
            var score = this.Uow.ScoreRepository.GetQuizScore(quizId);
            ThrowIf.NotFound(score);

            return Ok(score);
        }

        [HttpGet("quizzes")]
        public IActionResult GetQuizzes()
        {
            return Ok(this.Uow.QuizRepository.Get());
        }
    }
}
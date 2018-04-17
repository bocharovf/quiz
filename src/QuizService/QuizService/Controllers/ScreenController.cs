using Microsoft.AspNetCore.Mvc;
using QuizService.BusinessLogic;
using QuizService.Filters;
using QuizService.Interfaces.Common;
using QuizService.Model;
using QuizService.Model.DataContract;

namespace QuizService.Controllers
{
    [QuizExceptionFilter]
    [Produces("application/json")]
    [Route("api")]
    public class ScreenController : Controller
    {
        private readonly IUnitOfWork Uow;

        public ScreenController(IUnitOfWork uow)
        {
            this.Uow = uow;
        }

        // GET /api/quiz-templates
        [HttpGet("quiz-templates")]
        public IActionResult GetQuizTemplates()
        {
            return Ok(this.Uow.QuizTemplateRepository.Get());
        }

        // GET /api/quiz-templates/{id}
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

        // GET /api/scores
        [HttpGet("scores")]
        public IActionResult GetScores()
        {
            return Ok(this.Uow.ScoreRepository.Get());
        }

        // GET /api/scores/{scoreId}
        [HttpGet("scores/{scoreId}")]
        public IActionResult GetScore(int scoreId)
        {
            Score score = this.Uow.ScoreRepository.GetByID(scoreId);
            ThrowIf.NotFound(score, scoreId);

            return Ok(score);
        }

        // GET /api/quizzes/{quizId}/scores
        [HttpGet("quizzes/{quizId}/scores")]
        public IActionResult GetQuizScore(int quizId)
        {
            var score = this.Uow.ScoreRepository.GetQuizScore(quizId);
            ThrowIf.NotFound(score);

            return Ok(score);
        }

        // GET /api/quizzes
        [HttpGet("quizzes")]
        public IActionResult GetQuizzes()
        {
            return Ok(this.Uow.QuizRepository.Get());
        }
    }
}
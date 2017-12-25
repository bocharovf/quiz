using QuizService.Model;
using QuizService.Model.DataContract;

namespace QuizService.Interfaces.Managers
{
    public interface IQuizFlowManager
    {
        Quiz StartNewQuiz(int quizTemplateId);
        QuizFlowCommand GetNextQuestion(int quizId);
    }
}

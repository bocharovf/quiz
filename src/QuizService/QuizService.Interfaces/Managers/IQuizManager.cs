using QuizService.DataContract.QuizFlow;
using QuizService.Model;

namespace QuizService.Interfaces.Managers
{
    public interface IQuizFlowManager
    {
        Quiz StartNewQuiz(int quizTemplateId);
        QuizFlowCommand GetNextQuestion(int quizId);
    }
}

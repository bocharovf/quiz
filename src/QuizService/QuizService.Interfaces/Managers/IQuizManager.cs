using QuizService.Model;
using QuizService.Model.DataContract;

namespace QuizService.Interfaces.Managers
{
    public interface IQuizFlowManager
    {
        Quiz StartNewQuiz(QuizTemplate quizTemplate);
        QuizFlowCommandContract GetNextQuestion(Quiz quizId);
        void AnswerQuestion(Question question, int answerTemplateId);
        void CompleteQuiz(Quiz quiz);
    }
}

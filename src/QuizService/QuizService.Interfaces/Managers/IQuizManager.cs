using QuizService.Model;
using QuizService.Model.DataContract;

namespace QuizService.Interfaces.Managers
{
    /// <summary>
    /// Provides methods for quiz flow management.
    /// </summary>
    public interface IQuizFlowManager
    {
        /// <summary>
        /// Starts new quiz from specified quiz template.
        /// </summary>
        /// <param name="quizTemplate">The quiz template for new quiz.</param>
        /// <returns>New quiz.</returns>
        Quiz StartNewQuiz(QuizTemplate quizTemplate);

        /// <summary>
        /// Gets next question of the specified quiz.
        /// </summary>
        /// <param name="quizId">The quiz to get question from.</param>
        /// <returns>Quiz flow command to manage quiz flow.</returns>
        QuizFlowCommandContract GetNextQuestion(Quiz quizId);

        /// <summary>
        /// Answers specified quiz question.
        /// </summary>
        /// <param name="question">The question to answer.</param>
        /// <param name="answerTemplateId">The answer template identifier.</param>
        void AnswerQuestion(Question question, int answerTemplateId);

        /// <summary>
        /// Completes the quiz.
        /// </summary>
        /// <param name="quiz">The quiz to complete.</param>
        void CompleteQuiz(Quiz quiz);
    }
}

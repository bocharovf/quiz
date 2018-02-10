using QuizService.Interfaces.Repository;
using System;

namespace QuizService.Interfaces.Common
{
    /// <summary>
    /// Unit of work interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IQuizRepository QuizRepository { get; }
        IQuizTemplateRepository QuizTemplateRepository { get; }
        IQuestionTemplateRepository QuestionTemplateRepository { get; }
        IScoreRepository ScoreRepository { get; }

        /// <summary>
        /// Saves changes in all repositories in single transacion.
        /// </summary>
        void Save();
    }
}
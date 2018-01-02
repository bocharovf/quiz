using QuizService.BusinessLogic.Exceptions;
using QuizService.BusinessLogic.Scores;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Managers;
using QuizService.Model;
using QuizService.Model.DataContract;
using System;
using System.Linq;

namespace QuizService.BusinessLogic.QuizFlow
{
    public class QuizFlowManager : IQuizFlowManager
    {
        private IUnitOfWork Uow;
        private IScoreCalculationFactory ScoreCalculationFactory;

        public QuizFlowManager(IUnitOfWork uow, IScoreCalculationFactory scoreCalculationFactory)
        {
            this.Uow = uow;
            this.ScoreCalculationFactory = scoreCalculationFactory;
        }

        public Quiz StartNewQuiz(QuizTemplate quizTemplate)
        {
            ThrowIf.Null(quizTemplate, nameof(quizTemplate));

            var quiz = new Quiz
            {
                TemplateId = quizTemplate.Id,
                DateStart = DateTime.Now
            };

            this.Uow.QuizRepository.Insert(quiz);
            this.Uow.Save();

            return quiz;
        }

        public QuizFlowCommandContract GetNextQuestion(Quiz quiz)
        {
            ThrowIf.Null(quiz, nameof(quiz));
            ThrowIf.Completed(quiz);

            Question currentQuestion = quiz.CurrentQuestion;

            QuizFlowCommandContract command;
            if (currentQuestion != null && !currentQuestion.IsAnswered)
            {
                command = this.GetCommandFromCurrentQuestion(quiz, currentQuestion);
            }
            else
            {
                command = this.CreateCommandFromNextQuestion(quiz, currentQuestion);
            }

            this.Uow.Save();
            command.HideAnswerCorrectness();
            return command;
        }

        private QuizFlowCommandContract GetCommandFromCurrentQuestion(Quiz quiz, Question currentQuestion)
        {
            var questionTemplate = this.Uow.QuestionTemplateRepository.GetByID(currentQuestion.TemplateId);
            var command = new QuizFlowCommandProceedContract(currentQuestion, questionTemplate);
            return command;
        }

        private QuizFlowCommandContract CreateCommandFromNextQuestion(Quiz quiz, Question currentQuestion)
        {
            int nextQuestionOrder = currentQuestion == null ? 1 : currentQuestion.Order + 1;
            var questionTemplate = this.Uow.QuestionTemplateRepository
                                           .GetQuestionTemplate(quiz.TemplateId, nextQuestionOrder);
            if (questionTemplate == null)
            {
                return new QuizFlowCommandFinishContract();
            }

            Question nextQuestion = new Question()
            {
                Quiz = quiz,
                TemplateId = questionTemplate.Id,
                Order = nextQuestionOrder,
                DateStart = DateTime.Now
            };
            quiz.Questions.Add(nextQuestion);

            return new QuizFlowCommandProceedContract(nextQuestion, questionTemplate);
        }
        
        public void AnswerQuestion(Question question, int answerTemplateId)
        {
            ThrowIf.Null(question, nameof(question));
            ThrowIf.Completed(question.Quiz);
            
            if (question.IsAnswered)
            {
                throw new QuizFlowException(
                    QuizFlowErrorCodes.QuestionAlreadyAnswered, 
                    "Question already answered.");
            }

            QuestionTemplate questionTemplate = this.Uow.QuestionTemplateRepository
                                                        .GetByID(question.TemplateId);
            ThrowIf.NotFound(questionTemplate, question.TemplateId);

            AnswerTemplate answerTemplate = questionTemplate.GetAnswer(answerTemplateId);
            ThrowIf.NotFound(answerTemplate, answerTemplateId);

            var answer = new Answer()
            {
                TemplateId = answerTemplateId,
                IsCorrect = answerTemplate.IsCorrect
            };
            question.Answers.Add(answer);
            question.DateEnd = DateTime.Now;
            this.Uow.Save();
        }

        public void CompleteQuiz(Quiz quiz)
        {
            ThrowIf.Null(quiz, nameof(quiz));
            ThrowIf.Completed(quiz);

            IScoreCalculationStrategy scoreCalculation = this.ScoreCalculationFactory.CreateStrategy(quiz);
            var score = scoreCalculation.CalculateScore(quiz);
            this.Uow.ScoreRepository.Insert(score);

            quiz.DateEnd = DateTime.Now;
            this.Uow.Save();
        }
    }
}

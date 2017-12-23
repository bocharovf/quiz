using QuizService.BusinessLogic.Exceptions;
using QuizService.DataContract.QuizFlow;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Managers;
using QuizService.Model;
using System;
using System.Linq;

namespace QuizService.BusinessLogic.QuizFlow
{
    public class QuizFlowManager : IQuizFlowManager
    {
        private IUnitOfWork Uow;

        public QuizFlowManager(IUnitOfWork uow)
        {
            this.Uow = uow;
        }

        public Quiz StartNewQuiz(int quizTemplateId)
        {
            QuizTemplate quizTemplate = this.Uow.QuizTemplateRepository.GetByID(quizTemplateId);
            if (quizTemplate == null)
                throw new EntityNotFoundException(typeof(QuizTemplate).Name, quizTemplateId);

            var quiz = new Quiz
            {
                Template = quizTemplate,
                DateStart = DateTime.Now
            };

            this.Uow.QuizRepository.Insert(quiz);
            this.Uow.Save();

            return quiz;
        }

        public QuizFlowCommand GetNextQuestion(int quizId)
        {
            Quiz quiz = this.Uow.QuizRepository.GetByID(quizId);
            if (quiz == null)
                throw new EntityNotFoundException(typeof(Quiz).Name, quizId);

            Question lastQuestion = quiz.LastQuestion;
            if (lastQuestion != null && !lastQuestion.IsAnswered)
            {
                throw new QuizFlowException(
                    QuizFlowErrorCodes.LastQuestionNotAnswered, 
                    "You must answer previous question."
                );
            }

            int nextQuestionOrder = lastQuestion == null ? 1 : lastQuestion.Order + 1;

            QuestionTemplate questionTemplate = this.Uow.QuizTemplateRepository.GetQuestionTemplate(quiz.TemplateId, nextQuestionOrder);
            if (questionTemplate == null)
            {
                return new QuizFlowCommandFinish();
            }            

            Question nextQuestion = new Question()
            {
                Template = questionTemplate,
                Order = nextQuestionOrder,
                DateStart = DateTime.Now                
            };

            quiz.Questions.Add(nextQuestion);
            this.Uow.Save();

            return new QuizFlowCommandProceed(nextQuestion);
        }
    }
}

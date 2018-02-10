using Microsoft.EntityFrameworkCore;
using QuizService.Common.Extensions;
using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess
{
    /// <summary>
    /// Performs database data initialization.
    /// </summary>
    public class DatabaseInitializer
    {
        /// <summary>
        /// Initializes database.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public static void InitializeDatabase(string connectionString)
        {
            using (var context = ApplicationDatabaseContextFactory.CreateContext(connectionString))
            {
#if DEBUG
                context.Database.EnsureDeleted();
#endif
                
                context.Database.Migrate();
                InitializeData(context);
                context.SaveChanges();
            }
        }

        private static void InitializeData(ApplicationDatabaseContext context)
        {
            if (context.QuizTemplates.Any())
            {
                return;
            }

            var question1 = new QuestionTemplate()
            {
                QuestionType = QuestionType.SingleRight,
                Text = "Who is the author of `Alice in Wonderland` ?"
            };
            var answerQ1A1 = new AnswerTemplate()
            {
                IsCorrect = true,
                Text = "Charles Lutwidge Dodgson"
            };
            var answerQ1A2 = new AnswerTemplate()
            {
                IsCorrect = false,
                Text = "Charles John Huffam Dickens"
            };
            var answerQ1A3 = new AnswerTemplate()
            {
                IsCorrect = false,
                Text = "Agatha Mary Clarissa Christie"
            };
            var answerQ1A4 = new AnswerTemplate()
            {
                IsCorrect = false,
                Text = "No author. It's a folklore."
            };
            question1.Answers.AddRange(new[] { answerQ1A1, answerQ1A2, answerQ1A3, answerQ1A4 });

            var question2 = new QuestionTemplate()
            {
                QuestionType = QuestionType.SingleRight,
                Text = "Which of these novels written by Lev Nikolayevich Tolstoy are the most latest ?"
            };
            var answerQ2A1 = new AnswerTemplate()
            {
                IsCorrect = true,
                Text = "Hadji Murat"
            };
            var answerQ2A2 = new AnswerTemplate()
            {
                IsCorrect = false,
                Text = "The Cossacks"
            };
            var answerQ2A3 = new AnswerTemplate()
            {
                IsCorrect = false,
                Text = "War and Peace"
            };
            var answerQ2A4 = new AnswerTemplate()
            {
                IsCorrect = false,
                Text = "Childhood"
            };
            question2.Answers.AddRange(new[] { answerQ2A1, answerQ2A2, answerQ2A3, answerQ2A4 });

            var quizTemplate = new QuizTemplate()
            {
                Title = "Simple literature quiz",
                Description = "Short demo quiz about literature.",
            };

            var quizQuestion1 = new QuizQuestionTemplate(quizTemplate, question1, 1) {
                Enabled = true
            };
            var quizQuestion2 = new QuizQuestionTemplate(quizTemplate, question2, 2)
            {
                Enabled = true
            };
            
            context.QuizTemplates.Add(quizTemplate);
            context.QuizQuestionTemplates.AddRange(new[] { quizQuestion1, quizQuestion2 });
        }
    }
}

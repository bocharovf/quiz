using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(string connectionString)
        {
            using (var context = ApplicationDatabaseContextFactory.CreateContext(connectionString))
            {
#if DEBUG
                context.Database.EnsureDeleted();
#endif

                context.Database.EnsureCreated();

#if DEBUG
                InitializeData(context);
#endif

                context.SaveChanges();
            }
        }

        private static void InitializeData(ApplicationDatabaseContext context)
        {
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
            question1.Answers.Add(answerQ1A1);
            question1.Answers.Add(answerQ1A2);
            question1.Answers.Add(answerQ1A3);
            question1.Answers.Add(answerQ1A4);

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
            question2.Answers.Add(answerQ2A1);
            question2.Answers.Add(answerQ2A2);
            question2.Answers.Add(answerQ2A3);
            question2.Answers.Add(answerQ2A4);

            var quizTemplate = new QuizTemplate()
            {
                Title = "Test quize template",
                Description = "Test quize template description",
            };

            var quizQuestion1 = new QuizQuestionTemplate(quizTemplate, question1, 1) {
                Enabled = true
            };
            var quizQuestion2 = new QuizQuestionTemplate(quizTemplate, question2, 2)
            {
                Enabled = true
            };
            
            context.QuizTemplates.Add(quizTemplate);
            context.QuizQuestionTemplates.Add(quizQuestion1);
            context.QuizQuestionTemplates.Add(quizQuestion2);
        }
    }
}

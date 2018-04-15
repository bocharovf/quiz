using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizService.Common.Extensions;
using QuizService.DataAccess.Auth;
using QuizService.Model;
using System.Linq;
using System.Threading.Tasks;

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
        /// <param name="adminEmail">Administrator email.</param>
        /// <param name="adminPassword">Administrator password.</param>
        /// <param name="userManager">User identity manager.</param>
        /// <param name="roleManager">Role identity manager.</param>
        public static async Task InitializeDatabase(
            string connectionString, string adminEmail, string adminPassword,
            UserManager<AspnetUser> userManager, RoleManager<AspnetRole> roleManager)
        {
            using (var context = ApplicationDatabaseContextFactory.CreateContext(connectionString))
            {
#if DEBUG
                context.Database.EnsureDeleted();
#endif
                
                context.Database.Migrate();

                InitializeData(context);
                await InitializeUsersAndRoles(adminEmail, adminPassword, userManager, roleManager);

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

        private static async Task InitializeUsersAndRoles(
            string adminEmail, string adminPassword, 
            UserManager<AspnetUser> userManager, RoleManager<AspnetRole> roleManager)
        {
            var adminRole = new AspnetRole(ApplicationRole.Admin);
            await roleManager.CreateAsync(adminRole);

            var userRole = new AspnetRole(ApplicationRole.User);
            await roleManager.CreateAsync(userRole);

            var adminUser = new AspnetUser("Admin")
            {
                Email = adminEmail
            };

            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRolesAsync(adminUser, new[] {
                ApplicationRole.Admin,
                ApplicationRole.User
            });
        }
    }
}

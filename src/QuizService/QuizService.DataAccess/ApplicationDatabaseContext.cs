using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizService.DataAccess.Auth;
using QuizService.Model;

namespace QuizService.DataAccess
{
    internal class ApplicationDatabaseContext : IdentityDbContext<AspnetUser, AspnetRole, int>
    {
        public ApplicationDatabaseContext(DbContextOptions options)
            : base(options) { }

        public DbSet<QuizTemplate> QuizTemplates { get; set; }
        public DbSet<QuestionTemplate> QuestionTemplates { get; set; }
        public DbSet<QuizQuestionTemplate> QuizQuestionTemplates { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuizQuestionTemplate>()
                .HasKey(qqt => new
                {
                    qqt.QuizTemplateId,
                    qqt.Order,
                    qqt.QuestionTemplateId
                });

            modelBuilder.Entity<QuizTemplate>()
                .Property(q => q.Title)
                .IsRequired();

            modelBuilder.Entity<QuestionTemplate>()
                .Property(q => q.Text)
                .IsRequired();
        }
    }
}

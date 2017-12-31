using Microsoft.EntityFrameworkCore;
using QuizService.Model;

namespace QuizService.DataAccess
{
    internal class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options) { }

        public DbSet<QuizTemplate> QuizTemplates { get; set; }
        public DbSet<QuestionTemplate> QuestionTemplates { get; set; }
        public DbSet<QuizQuestionTemplate> QuizQuestionTemplates { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

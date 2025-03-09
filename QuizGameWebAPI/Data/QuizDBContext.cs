using Microsoft.EntityFrameworkCore;
using QuizGameWebAPI.Models;

namespace QuizGameWebAPI.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserQuizModel> UserQuiz { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionTypeModel> QuestionTypes { get; set; }
        public DbSet<OptionModel> Options { get; set; }
    }

}

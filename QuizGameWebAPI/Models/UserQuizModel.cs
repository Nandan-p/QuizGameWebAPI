using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizGameWebAPI.Models
{
    [Table("UserQuiz")]
    public class UserQuizModel
    {
        [Column("Id")]
        [Key]
        public long UserQuizId { get; set; }
        public long UserId { get; set; }
        public long AttemptedQuestionId { get; set; }
        public long SelectedOptionId { get; set; }
        public bool IsSelectedOptionCorrect { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}

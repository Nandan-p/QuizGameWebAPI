using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizGameWebAPI.Models
{
    [Table("Question")]
    public class QuestionModel
    {
        [Column("Id")]
        [Key]
        public long QuestionId { get; set; }
        public string? Question { get; set; }
        public int QuestionTypeId { get; set; }
        public QuestionTypeModel? QuestionType  { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}

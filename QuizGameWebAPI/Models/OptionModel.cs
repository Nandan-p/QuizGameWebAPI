using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizGameWebAPI.Models
{
    [Table("Option")]
    public class OptionModel
    {
        [Column("Id")]
        [Key]
        public long OptionId { get; set; }
        public string? Option { get; set; }
        public long QuestionId { get; set; }
        public QuestionModel? Question { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}

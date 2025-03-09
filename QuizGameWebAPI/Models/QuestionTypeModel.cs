using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizGameWebAPI.Models
{
    [Table("QuestionType")]
    public class QuestionTypeModel
    {
        [Column("Id")]
        [Key]
        public int QuestionTypeId { get; set; }
        public string? QuestionType { get; set; }
        public bool Isactive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}

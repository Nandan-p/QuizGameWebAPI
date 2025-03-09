namespace QuizGameWebAPI.Models.Common
{
    public class Option
    {
        public long OptionId { get; set; }
        public string? OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsActive { get; set; }
        public long QuestionId { get; set; }
    }
}

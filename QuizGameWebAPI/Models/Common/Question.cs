namespace QuizGameWebAPI.Models.Common
{
    public class Question
    {
        public long QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public bool IsActive { get; set; }
        public int QuestioTypeId { get; set; }
        public List<Option>? Options { get; set; }
    }
}

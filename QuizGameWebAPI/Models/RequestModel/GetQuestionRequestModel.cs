namespace QuizGameWebAPI.Models.RequestModel
{
    public class GetQuestionRequestModel
    {
        public long QuestionId { get; set; }
        public bool IsActive { get; set; }
        public int QuestionTypeId  { get; set; }
    }
}

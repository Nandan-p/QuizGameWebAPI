namespace QuizGameWebAPI.Models.RequestModel
{
    public class UserQuizRequestModel
    {        
        public long UserId { get; set; }
        public long AttemptedQuestionId { get; set; }
        public long SelectedOptionId { get; set; }
    }
}

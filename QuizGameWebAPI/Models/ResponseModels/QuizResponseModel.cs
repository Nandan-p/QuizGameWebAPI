namespace QuizGameWebAPI.Models.ResponseModels
{
    public class QuizResponseModel
    {
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string? ResponseMessage { get; set; }
    }
}

namespace QuizGameWebAPI.Models.ResponseModels
{
    public class BooleanResponseModel
    {
        public bool IsCorrect { get; set; }
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string? ResponseMessage { get; set; }
    }
}

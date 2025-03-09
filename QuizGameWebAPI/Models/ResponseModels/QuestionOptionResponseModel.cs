
using QuizGameWebAPI.Models.Common;

namespace QuizGameWebAPI.Models.ResponseModels
{
    public class QuestionOptionResponseModel
    {
        public bool IsSuccess { get; set; }
        public Question? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

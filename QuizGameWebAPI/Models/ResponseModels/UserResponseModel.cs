
using Microsoft.OpenApi.Writers;

namespace QuizGameWebAPI.Models.ResponseModels
{
    public class UserResponseModel
    {
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class User
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int Gender { get; set; }
        public bool IsAcive { get; set; }
    }
}

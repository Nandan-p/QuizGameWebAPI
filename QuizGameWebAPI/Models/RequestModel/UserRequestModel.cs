namespace QuizGameWebAPI.Models.RequestModel
{
    public class UserRequestModel
    {
        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Gender { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}

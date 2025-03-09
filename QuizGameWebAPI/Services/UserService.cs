using AutoMapper;
using QuizGameWebAPI.Models;
using QuizGameWebAPI.Models.RequestModel;
using QuizGameWebAPI.Models.ResponseModels;
using QuizGameWebAPI.Repositories;
using System.Threading.Tasks;

namespace QuizGameWebAPI.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly QuestionRepository _questionRepository;
        private readonly OptionRepository _optionRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper, QuestionRepository questionRepository, OptionRepository optionRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
        }

        public Task<IEnumerable<UserModel>> GetAllUsers() => _userRepository.GetAllAsync();
        public Task<UserModel> GetUserByID(long userId) => _userRepository.GetByIdAsync(userId);
        public async Task<IEnumerable<UserQuizModel>> GetAllUserQuizes() => await _userRepository.GetAllUserQuizAsync();
        public async Task<IEnumerable<UserQuizModel>> GetAllUserQuizesByUserId(long userId)
        {
            var userQuizList = await _userRepository.GetAllUserQuizAsync();
            return userQuizList.Where(w => w.UserId == userId).ToList() ?? new();
        }
        public async Task<UserResponseModel> CreateUpdateUser(UserRequestModel request)
        {
            var response = new UserResponseModel();
            try
            {
                if (request == null)
                    return response;

                //Update User
                if (request.UserId > 0)
                {
                    var user = await _userRepository.GetByIdAsync(request.UserId);
                    if (user == null)
                    {
                        response.ErrorMessage = "User Not Found";
                        return response;
                    }
                    var updateUser = _mapper.Map<UserModel>(request);

                    if (updateUser == null)
                        return response;

                    var output = await _userRepository.UpdateAsync(updateUser);
                    if (output != null)
                    {
                        response.Data = output;
                        response.IsSuccess = true;
                        response.ErrorMessage = "Updated Successfully";
                    }
                }

                //Add user
                else
                {
                    if (await IsUserAlreadyExists(request.FirstName))
                    {
                        response.ErrorMessage = "User Already Exist";
                        return response;
                    }
                    var addUser = _mapper.Map<UserModel>(request);
                    if (addUser == null)
                        return response;

                    var output = await _userRepository.AddAsync(addUser);
                    if (output.UserId > 0)
                    {
                        response.Data = output;
                        response.IsSuccess = true;
                        response.ErrorMessage = "Added Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        public async Task<BooleanResponseModel> CreateUserQuiz(UserQuizRequestModel request)
        {
            var response = new BooleanResponseModel();
            try
            {
                var IsUser = await IsUserExists(request.UserId);
                if (!IsUser)
                {
                    response.ResponseMessage = "User Not Exist";
                    return response;
                }

                var IsQuesion = await IsQuesionExists(request.AttemptedQuestionId);
                if (!IsQuesion)
                {
                    response.ResponseMessage = "Question Not Exist";
                    return response;
                }

                var IsOption = await IsOptionExists(request.SelectedOptionId);
                if (!IsOption)
                {
                    response.ResponseMessage = "Option Not Exist";
                    return response;
                }

                var userQuiz = _mapper.Map<UserQuizModel>(request);

                if (userQuiz == null)
                    return response;

                var IsSelectedCorrect = await IsSelectedOptionCorrect(request.AttemptedQuestionId, request.SelectedOptionId);

                if (IsSelectedCorrect)
                    userQuiz.IsSelectedOptionCorrect = true;

                var output = await _userRepository.AddUserQuizAsync(userQuiz);
                if (output.UserQuizId > 0)
                {
                    response.IsCorrect = output.IsSelectedOptionCorrect;
                    response.IsSuccess = true;
                    response.Data = output;
                    response.ResponseMessage = "UserQuiz Added Succesfully";
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
        private async Task<bool> IsUserExists(long userId)
        {
            var user = await GetUserByID(userId);
            return user != null && user.IsActive;
        }
        private async Task<bool> IsUserAlreadyExists(string userName)
        {
            var user = await GetAllUsers();
            return user != null && user.Any(a => a.FirstName.Equals(userName));
        }
        private async Task<bool> IsQuesionExists(long questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            return question != null && question.IsActive;
        }
        private async Task<bool> IsOptionExists(long optionId)
        {
            var option = await _optionRepository.GetByIdAsync(optionId);
            return option != null && option.IsActive;
        }

        private async Task<bool> IsSelectedOptionCorrect(long questionId, long optionId)
        {
            var options = await _optionRepository.GetByQuestioIdAsync(questionId);

            var correctOption = options.FirstOrDefault(w => w.IsCorrect);

            return correctOption != null && correctOption.OptionId == optionId;
        }
    }

}

using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using QuizGameWebAPI.Models;
using QuizGameWebAPI.Models.Common;
using QuizGameWebAPI.Models.RequestModel;
using QuizGameWebAPI.Models.ResponseModels;
using QuizGameWebAPI.Repositories;
using System.Threading.Tasks;

namespace QuizGameWebAPI.Services
{
    public class QuestionService
    {
        private readonly QuestionRepository _questionRepository;
        private readonly OptionRepository _optionRepository;
        private readonly IMapper _mapper;

        public QuestionService(QuestionRepository questionRepository, OptionRepository optionRepository,IMapper mapper)
        {
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
            _mapper = mapper;
        }
           
        public Task<IEnumerable<QuestionModel>> GetAllQuestions() => _questionRepository.GetAllAsync();
        public Task<QuestionModel> GetQuestionById(long id) => _questionRepository.GetByIdAsync(id);
        public async Task<QuestionOptionResponseModel> GetQuestionOption(GetQuestionRequestModel request)
        {
            var response = new QuestionOptionResponseModel();
            response.ErrorMessage = "No data found";
            try
            {
                var question = await _questionRepository.GetByIdAsync(request.QuestionId);
                var options = await _optionRepository.GetByQuestioIdAsync(request.QuestionId);
                if (question == null && options.Count() == 0)
                    return response;

                    response.Data = _mapper.Map<Question>(question ?? new());
                    response.Data.Options = _mapper.Map<List<Option>>(options);

                if (response.Data != null && response.Data.Options.Any())
                {
                    response.ErrorMessage = string.Empty;
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        public async Task<BooleanResponseModel> CreateUpdateQuestion(QuestionOptionRequestModel request)
        {
            var response = new BooleanResponseModel();
            try
            {
                if (request == null)
                    return response;

                var options = request.Question?.Options;
                var question = request.Question;
                //Update Question
                if (question!= null && question?.QuestionId > 0)
                {
                    var existquestion = await _questionRepository.GetByIdAsync(question.QuestionId);
                    if (existquestion != null)
                    {
                        var updateQuestion = _mapper.Map<QuestionModel>(question);

                        var output = await _questionRepository.UpdateAsync(updateQuestion);
                        if (output > 0)
                        {
                            response.IsSuccess = true;
                            response.ResponseMessage = "Question Updated Successfully";
                        }
                        if (options != null && options.Count > 0)
                        {
                            if (options?.Count(a => a.IsCorrect) > 1)
                            {
                                response.ResponseMessage = "There is more that one correct option";
                                return response;
                            }
                              await CreateUpdateOption(options);
                        }
                    }
                }
                //Add Question
                else
                {
                    var mapQuestion = _mapper.Map<QuestionModel>(request);

                    if (mapQuestion == null)
                        return response;

                    var output = await _questionRepository.AddAsync(mapQuestion);
                    if (output > 0)
                    {
                        response.IsSuccess = true;
                        response.ResponseMessage = "Added Successfully";
                    }
                    if (options != null && options.Count > 0)
                    {
                        foreach (var option in options)
                        {
                            option.QuestionId = output;
                        }
                            await CreateUpdateOption(options);
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
                return response;
        }
        private async Task<BooleanResponseModel> CreateUpdateOption(List<Option> requests)
        {
            var response = new BooleanResponseModel();
            try
            {
                if (requests == null || !requests.Any())
                    return response;

                var optionsToUpdate = new List<OptionModel>();
                var optionsToAdd = new List<OptionModel>();

                foreach (var request in requests)
                {
                    var optionModel = _mapper.Map<OptionModel>(request);
                    if (optionModel.OptionId > 0)
                    {
                        optionsToUpdate.Add(optionModel);
                    }
                    else
                    {
                        optionsToAdd.Add(optionModel);
                    }
                }

                if (optionsToUpdate.Any())
                {
                  await _optionRepository.UpdateRangeAsync(optionsToUpdate);
                }

                // Add new options
                if (optionsToAdd.Any())
                {
                   await _optionRepository.AddRangeAsync(optionsToAdd);
                }

                response.IsSuccess = true;
                response.ResponseMessage = "Options saved/updated successfully";
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
                return response;
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using QuizGameWebAPI.Models.Common;
using QuizGameWebAPI.Models.RequestModel;
using QuizGameWebAPI.Services;

namespace QuizGameWebAPI.Controllers;

[ApiController]
[Route("Quiz")]
public class QuizGameController(ILogger<QuizGameController> logger,QuestionService questionService) : ControllerBase
{
    [HttpPost("CreateQuestion")]
    public async Task<IActionResult> CreateQuestion(QuestionOptionRequestModel requestModel)
    {
        try
        {
            var result = await questionService.CreateUpdateQuestion(requestModel);
            return Ok(result);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest(ex);
        }
    }
    
    [HttpGet("GetQuestionOption")]
    public async Task<IActionResult> GetQuestionOption([FromQuery]GetQuestionRequestModel requestModel)
    {
        try
        {
            var result = await questionService.GetQuestionOption(requestModel);
            return Ok(result);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest(ex);
        }
    }
}

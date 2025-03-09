using Microsoft.AspNetCore.Mvc;
using QuizGameWebAPI.Models.RequestModel;
using QuizGameWebAPI.Services;

namespace QuizGameWebAPI.Controllers;

[ApiController]
[Route("User")]
public class UserController(ILogger<QuizGameController> logger,UserService userService) : ControllerBase
{
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(UserRequestModel requestModel)
    {
        try
        {
            var result = await userService.CreateUpdateUser(requestModel);
            return Ok(result);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPut("CreateUserQuiz")]
    public async Task<IActionResult> CreateUserQuiz(UserQuizRequestModel requestModel)
    {
        try
        {
            var result = await userService.CreateUserQuiz(requestModel);
            return Ok(result);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest(ex);
        }
    }
}

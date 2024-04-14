using Domain.Requests;
using Domain.Responses;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers;

[ApiController]
[Route("users")]
public class UserController
{

    private readonly IApplicationService _applicationService;

    public UserController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    [HttpGet]
    [Route("{id:guid}/currentapplication")]
    public async Task<ActionResult<GetResponse>> GetUncommitted([FromRoute] Guid id)
    {
        return ToReturn(await _applicationService.GetUncommitted(id));
    }
    
    private ActionResult ToReturn(Result result)
    {
        if (result is Result.Success success)
        {
            return new OkObjectResult(success.GetResponse);
        }

        if (result is Result.SuccessWithMoreObjects successWithMoreObjects)
        {
            return new OkObjectResult(successWithMoreObjects.GetResponses);
        }
        if (result is Result.Error error)
        {
            return new BadRequestObjectResult(error.Description);
        }

        return new BadRequestObjectResult("Unknown problem");
    }
    
}
using Domain.Responses;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers;

[ApiController]
[Route("applications")]
public class UserController
{

    private readonly IApplicationService _applicationService;

    public UserController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    [HttpGet]
    [Route("{id:guid}/currentapplication")]
    public Task<GetResponse?> GetUncommitted([FromRoute] Guid id)
    {
        return _applicationService.GetUncommitted(id);
    }
}
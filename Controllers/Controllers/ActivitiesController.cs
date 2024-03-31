using Domain.Responses;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers;
[ApiController]
[Route("activities")]
public class ActivitiesController
{

    private readonly IApplicationService _applicationService;

    public ActivitiesController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    [HttpGet]
    public List<GetActivity> Get()
    {
        return _applicationService.GetActivities();
    }
}
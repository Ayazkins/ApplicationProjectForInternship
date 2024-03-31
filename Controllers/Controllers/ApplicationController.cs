using System.Net;
using DAL.Exceptions;
using Domain.Requests;
using Domain.Responses;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers;

[ApiController]
[Route("applications")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost]
    public async Task<AddResponse> Post([FromBody] AddRequest addRequest)
    {
        return await _applicationService.Post(addRequest);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<UpdateResponse> Put([FromRoute] Guid id, [FromBody] UpdateRequest updateRequest)
    {
        return await _applicationService.Put(id, updateRequest);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await _applicationService.Delete(id);
    }

    [HttpPost]
    [Route("{id:guid}/commit")]
    public async Task Commit([FromRoute] Guid id)
    {
        await _applicationService.Send(id);
    }

    [HttpGet]
    public async Task<List<GetResponse>> GetAfter([FromQuery(Name = "submittedAfter")] DateTime? submittedAfter,
        [FromQuery(Name = "unsubmittedOlder")] DateTime? unsubmittedOlder)
    {
        if (submittedAfter.HasValue)
        {
            return await _applicationService.GetAfter(submittedAfter.Value);
        }

        if (unsubmittedOlder.HasValue)
        {
            return await _applicationService.GetOlder(unsubmittedOlder.Value);
        }

        throw new HttpRequestException("No value");
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<GetResponse?> Get([FromRoute] Guid id)
    {
        return await _applicationService.Get(id);
    }
}
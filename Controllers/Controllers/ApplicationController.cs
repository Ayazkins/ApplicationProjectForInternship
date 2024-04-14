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
    public async Task<ActionResult<GetResponse>> Post([FromBody] AddRequest addRequest)
    {
        return ToReturn(await _applicationService.Post(addRequest));
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult<GetResponse>> Put([FromRoute] Guid id, [FromBody] UpdateRequest updateRequest)
    {
        return ToReturn(await _applicationService.Put(id, updateRequest));
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ActionResult<string>> Delete([FromRoute] Guid id)
    {
        return ToReturn(await _applicationService.Delete(id));
    }

    [HttpPost]
    [Route("{id:guid}/commit")]
    public async Task<ActionResult<string>> Commit([FromRoute] Guid id)
    {
       return ToReturn(await _applicationService.Send(id));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetResponse>>> GetAfter([FromQuery(Name = "submittedAfter")] DateTime? submittedAfter,
        [FromQuery(Name = "unsubmittedOlder")] DateTime? unsubmittedOlder)
    {
        if (submittedAfter == null && unsubmittedOlder == null || submittedAfter != null && unsubmittedOlder != null)
        {
            throw new BadHttpRequestException("Request must have one timestamp");
        }

        if (submittedAfter.HasValue)
        {
            return ToReturn(await _applicationService.GetAfter(submittedAfter.Value));
        }
        return ToReturn(await _applicationService.GetOlder(unsubmittedOlder.Value));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<GetResponse>> Get([FromRoute] Guid id)
    {
        return ToReturn(await _applicationService.Get(id));
    }

    private ActionResult ToReturn(Result result)
    {
        if (result is Result.Success success)
        {
            if (success.GetResponse == null)
            {
                return Ok();
            }
            return Ok(success.GetResponse);
        }

        if (result is Result.SuccessWithMoreObjects successWithMoreObjects)
        {
            return Ok(successWithMoreObjects.GetResponses);
        }
        if (result is Result.Error error)
        {
            return BadRequest(error.Description);
        }

        return BadRequest();
    }
}
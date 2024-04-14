using Domain.Requests;
using Domain.Responses;

namespace Domain.Services;

public interface IApplicationService
{
    Task<Result> Post(AddRequest addRequest);

    Task<Result> Put(Guid id, UpdateRequest updateRequest);

    Task<Result>  Delete(Guid id);

    Task<Result>  Send(Guid id);

    Task<Result> GetAfter(DateTime dateTime);

    Task<Result> GetOlder(DateTime dateTime);
    
    Task<Result> Get(Guid id);

    Task<Result> GetUncommitted(Guid id);

    List<GetActivity> GetActivities();

}
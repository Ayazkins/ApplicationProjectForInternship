using Domain.Requests;
using Domain.Responses;

namespace Domain.Services;

public interface IApplicationService
{
    Task<AddResponse> Post(AddRequest addRequest);

    Task<UpdateResponse> Put(Guid id, UpdateRequest updateRequest);

    Task<Task> Delete(Guid id);

    Task Send(Guid id);

    Task<List<GetResponse>> GetAfter(DateTime dateTime);

    Task<List<GetResponse>> GetOlder(DateTime dateTime);
    
    Task<GetResponse?> Get(Guid id);

    Task<GetResponse?> GetUncommitted(Guid id);

    List<GetActivity> GetActivities();

}
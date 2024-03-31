using Domain.Requests;
using Domain.Responses;

namespace Domain.Interfaces.Repository;

public interface IApplicationRepository
{
    Task<AddResponse> AddAsync(AddRequest addRequest);

    Task<UpdateResponse> UpdateAsync(Guid id, UpdateRequest updateRequest);

    Task DeleteAsync(Guid id);

    Task SendAsync(Guid id);

    Task<List<GetResponse>> GetAfterAsync(DateTime time);
    
    Task<List<GetResponse>> GetOlderAsync(DateTime time);
    
    Task<GetResponse?> GetAsync(Guid id);

    Task<GetResponse?> GetUncommitted(Guid author);

    Task<bool> IsCommitted(Guid id);

}
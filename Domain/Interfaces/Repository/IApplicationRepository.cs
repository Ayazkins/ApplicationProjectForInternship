using Domain.Entity;
using Domain.Requests;
using Domain.Responses;

namespace Domain.Interfaces.Repository;

public interface IApplicationRepository
{
    Task<Application> AddAsync(Application application);

    Task<Application> UpdateAsync(Application application);

    Task DeleteAsync(Application application);
    
    Task<List<Application>> GetAfterAsync(DateTime time);
    
    Task<List<Application>> GetOlderAsync(DateTime time);
    
    Task<Application?> GetAsync(Guid id);

    Task<Application?> GetUncommitted(Guid author);


    Task<bool> AuthorExists(Guid id);
}
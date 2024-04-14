using Domain.Entity;
using Domain.Interfaces.Repository;
using Domain.Requests;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace DAL.ApplicationRepository;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ApplicationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Application> AddAsync(Application application)
    {
        await _dbContext
            .Applications
            .AddAsync(application);
        await _dbContext.SaveChangesAsync();

        return application;
    }

    public async Task<Application> UpdateAsync(Application application)
    { 
        await _dbContext.SaveChangesAsync();
        return application;
    }

    public async Task DeleteAsync(Application application)
    {
        _dbContext.Applications.Remove(application);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SendAsync(Guid id)
    {
        await _dbContext.Applications.Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.SendAt, a => DateTime.Now.ToUniversalTime()));
    }

    public async Task<List<Application>> GetAfterAsync(DateTime time)
    {
        var applicationEntities =
            await _dbContext.Applications.Where(a => a.SendAt != null && a.SendAt.Value > time.ToUniversalTime()).ToListAsync();
        
        return applicationEntities;
    }

    public async Task<List<Application>> GetOlderAsync(DateTime time)
    {
        var applicationEntities =
            await _dbContext.Applications.Where(a => a.CreatedAt > time.ToUniversalTime() && a.SendAt == null).ToListAsync();
        
        return applicationEntities;
    }

    public async Task<Application?> GetAsync(Guid id)
    {
        var application = await _dbContext.Applications.SingleOrDefaultAsync(a => a.Id == id);

        return application;
    }
    public async Task<Application?> GetUncommitted(Guid author)
    {
       var application =  await _dbContext.Applications.SingleOrDefaultAsync(a => a.Author == author && a.SendAt == null);
       return application;
    }

    public async Task<bool> AuthorExists(Guid id)
    {
        var application = await _dbContext.Applications.SingleOrDefaultAsync(a => a.Author == id);
        return application != null;
    }
}
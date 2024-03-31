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

    public async Task<AddResponse> AddAsync(AddRequest addRequest)
    {
        var application = new Application
        {
            Author = addRequest.Author,
            ActivityType = addRequest.ActivityType,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            Description = addRequest.Description,
            Name = addRequest.Name,
            Outline = addRequest.Outline
        };
        await _dbContext
            .Applications
            .AddAsync(application);
        await _dbContext.SaveChangesAsync();

        return new AddResponse(
            application.Id,
            application.Author,
            application.ActivityType,
            application.Name,
            application.Description,
            application.Outline
        );
    }

    public async Task<UpdateResponse> UpdateAsync(Guid id, UpdateRequest updateRequest)
    {
        await _dbContext.Applications
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Outline, a => updateRequest.Outline)
                .SetProperty(a => a.Description, a => updateRequest.Description)
                .SetProperty(a => a.ActivityType, a => updateRequest.ActivityType)
                .SetProperty(a => a.Name, a => updateRequest.Name)
                .SetProperty(a => a.UpdatedAt, a => DateTime.Now.ToUniversalTime()));
        var application = await _dbContext.Applications.SingleOrDefaultAsync(a => a.Id == id);
        return new UpdateResponse(
            application.Id,
            application.Author,
            application.ActivityType,
            application.Name,
            application.Description,
            application.Outline
        );
    }

    public async Task DeleteAsync(Guid id)
    {
        await _dbContext.Applications.Where(a => a.Id == id).ExecuteDeleteAsync();
    }

    public async Task SendAsync(Guid id)
    {
        await _dbContext.Applications.Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.SendAt, a => DateTime.Now.ToUniversalTime()));
    }

    public async Task<List<GetResponse>> GetAfterAsync(DateTime time)
    {
        var applicationEntities =
            await _dbContext.Applications.Where(a => a.SendAt != null && a.SendAt.Value > time.ToUniversalTime()).ToListAsync();

        var response = applicationEntities.Select(application => new GetResponse(
                application.Id,
                application.Author,
                application.ActivityType,
                application.Name,
                application.Description,
                application.Outline))
            .ToList();
        return response;
    }

    public async Task<List<GetResponse>> GetOlderAsync(DateTime time)
    {
        var applicationEntities =
            await _dbContext.Applications.Where(a => a.CreatedAt > time.ToUniversalTime() && a.SendAt == null).ToListAsync();

        var response = applicationEntities.Select(application => new GetResponse(
                application.Id,
                application.Author,
                application.ActivityType,
                application.Name,
                application.Description,
                application.Outline))
            .ToList();
        return response;
    }

    public async Task<GetResponse?> GetAsync(Guid id)
    {
        var application = await _dbContext.Applications.SingleOrDefaultAsync(a => a.Id == id);
        if (application == null)
        {
            return null;
        }
        return new GetResponse(
            application.Id,
            application.Author,
            application.ActivityType,
            application.Name,
            application.Description,
            application.Outline
        );
    }

    public async Task<bool> IsCommitted(Guid id)
    {
        var application = await _dbContext.Applications.SingleOrDefaultAsync(a => a.Id == id && a.SendAt != null);
        return application != null;
    }

    public async Task<GetResponse?> GetUncommitted(Guid author)
    {
       var application =  await _dbContext.Applications.SingleOrDefaultAsync(a => a.Author == author && a.SendAt == null);
       if (application == null)
       {
           return null;
       }
       return new GetResponse(
           application.Id,
           application.Author,
           application.ActivityType,        
           application.Name,
           application.Description,
           application.Outline
           );
    }
}
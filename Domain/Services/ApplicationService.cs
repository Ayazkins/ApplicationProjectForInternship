using System.Net;
using DAL.ApplicationValidator;
using Domain.Entity;
using Domain.Interfaces.Repository;
using Domain.Requests;
using Domain.Responses;
using Microsoft.VisualBasic;

namespace Domain.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly UnsubmittedApplicationValidator _unsubmittedApplicationValidator;
    private readonly ApplicationValidator _applicationValidator;

    public ApplicationService(IApplicationRepository applicationRepository, UnsubmittedApplicationValidator unsubmittedApplicationValidator, ApplicationValidator applicationValidator)
    {
        _applicationRepository = applicationRepository;
        _unsubmittedApplicationValidator = unsubmittedApplicationValidator;
        _applicationValidator = applicationValidator;
    }
    public async Task<Result> Post(AddRequest addRequest)
    {
        var application = new Application().CreateRequest(addRequest);
        var result = await _unsubmittedApplicationValidator.ValidateAsync(application);

        if (!result.IsValid)
        {
            return new Result.Error(HttpStatusCode.BadRequest,
                Strings.Join(result.Errors.Select(e => e.ErrorMessage).ToArray(), "\n"));
        }

        if (await _applicationRepository.AuthorExists(application.Author))
        {
            return new Result.Error(HttpStatusCode.Conflict,
                "Author already has application");
        }
        await _applicationRepository.AddAsync(application);

        return new Result.Success(ToDto(application));
    }

    public async Task<Result> Put(Guid id, UpdateRequest updateRequest)
    {
        var application = await _applicationRepository.GetAsync(id);
        if (application == null)
        {
            return new Result.Error(HttpStatusCode.NotFound, "Application doesn't exist");
        }
        application.UpdateRequest(updateRequest);
        var result = await _unsubmittedApplicationValidator.ValidateAsync(application);
        if (!result.IsValid)
        {
            return new Result.Error(HttpStatusCode.BadRequest,
                Strings.Join(result.Errors.Select(e => e.ErrorMessage).ToArray(), "\n"));
        }
        
        return new Result.Success(ToDto(await _applicationRepository.UpdateAsync(application)));
    }

    public async Task<Result> Delete(Guid id)
    {
        var application = await _applicationRepository.GetAsync(id);
        if (application == null)
        {
            return new Result.Error(HttpStatusCode.NotFound, "Application doesn't exist");
        }

        var result = await _unsubmittedApplicationValidator.ValidateAsync(application);
        if (!result.IsValid)
        {
            return new Result.Error(HttpStatusCode.BadRequest,
                Strings.Join(result.Errors.Select(e => e.ErrorMessage).ToArray(), "\n"));
            
        }
        
        await _applicationRepository.DeleteAsync(application);
        return new Result.Success(null);

    }

    public async Task<Result> Send(Guid id)
    {
        var application= await _applicationRepository.GetAsync(id);
        if (application == null)
        {
            return new Result.Error(HttpStatusCode.NotFound, "Application doesn't exist");
        }

        var result = await _applicationValidator.ValidateAsync(application);
        
        if (!result.IsValid)
        {
            return new Result.Error(HttpStatusCode.BadRequest,
                Strings.Join(result.Errors.Select(e => e.ErrorMessage).ToArray(), "\n"));        }

        application.Commit();
        await _applicationRepository.UpdateAsync(application);
        return new Result.Success(null);
    }

    public async Task<Result> GetAfter(DateTime dateTime)
    {
        var applications = await _applicationRepository.GetAfterAsync(dateTime);
        return new Result.SuccessWithMoreObjects(applications.Select(a => ToDto(a)).ToList());

    }

    public async Task<Result> GetOlder(DateTime dateTime)
    {
        var applications = await _applicationRepository.GetOlderAsync(dateTime);
        return new Result.SuccessWithMoreObjects(applications.Select(a => ToDto(a)).ToList());

    }

    public async Task<Result> Get(Guid id)
    {
        var application = await _applicationRepository.GetAsync(id);
        if (application == null)
        {
            return new Result.Error(HttpStatusCode.NotFound, "Application doesn't exist");
        }
        return new Result.Success(ToDto(application));
    }
    
    public async Task<Result> GetUncommitted(Guid id)
    {
        var application = await _applicationRepository.GetUncommitted(id);

        if (application == null)
        {
            return new Result.Error(HttpStatusCode.NotFound, "Application doesn't exist");
        }

        return new Result.Success(ToDto(application));
    }

    public List<GetActivity> GetActivities()
    {
        List<GetActivity> list = new List<GetActivity>
        {
            new(Activity.Discussion, "Дискуссия / круглый стол, 40-50 минут"),
            new(Activity.Masterclass, "Мастеркласс, 1-2 часа"),
            new(Activity.Report, "Доклад, 35-45 минут")
        };
        return list;
    }

    private GetResponse ToDto(Application application)
    {
        return new GetResponse(application.Id,
            application.Author,
            application.ActivityType,
            application.Name,
            application.Description,
            application.Outline);
    }
}
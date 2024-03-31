using DAL.ApplicationValidator;
using DAL.Exceptions;
using Domain.Entity;
using Domain.Interfaces.Repository;
using Domain.Requests;
using Domain.Responses;

namespace Domain.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IValidator _validator;

    public ApplicationService(IApplicationRepository applicationRepository, IValidator validator)
    {
        _applicationRepository = applicationRepository;
        _validator = validator;
    }
    public async Task<AddResponse> Post(AddRequest addRequest)
    {
        if (!_validator.IsAtLeastOneFiled(addRequest))
        {
            throw new NotEnoughFieldsOccuredException("Add more information");
        }
        return await _applicationRepository.AddAsync(addRequest);
    }

    public async Task<UpdateResponse> Put(Guid id, UpdateRequest updateRequest)
    {
        if (!_validator.IsAtLeastOneFiled(updateRequest))
        {
            throw new NotEnoughFieldsOccuredException("Fill more fields");
        }

        if (await _applicationRepository.IsCommitted(id))
        {
            throw new ApplicationAlreadyCommittedException("You can't update committed applications");
        }

        if (await _applicationRepository.GetAsync(id) == null)
        {
            throw new NoSuchApplicationException("Application doesn't exist");
        }
        return await _applicationRepository.UpdateAsync(id, updateRequest);
    }

    public async Task<Task> Delete(Guid id)
    {
        if (await _applicationRepository.IsCommitted(id))
        {
            throw new ApplicationAlreadyCommittedException("You can't delete committed applications");
        }
        if (await _applicationRepository.GetAsync(id) == null)
        {
            throw new NoSuchApplicationException("Application doesn't exist");
        }
        return _applicationRepository.DeleteAsync(id);
    }

    public async Task Send(Guid id)
    {
        if (!_validator.IsAllFieldsOccured(await _applicationRepository.GetAsync(id)))
        {
            throw new NotEnoughFieldsOccuredException("Fill all fields");
        }
        if (await _applicationRepository.GetAsync(id) == null)
        {
            throw new NoSuchApplicationException("Application doesn't exist");
        }
        await _applicationRepository.SendAsync(id);
    }

    public async Task<List<GetResponse>> GetAfter(DateTime dateTime)
    {
        return await _applicationRepository.GetAfterAsync(dateTime);
    }

    public async Task<List<GetResponse>> GetOlder(DateTime dateTime)
    {
        return await _applicationRepository.GetOlderAsync(dateTime);
    }

    public async Task<GetResponse?> Get(Guid id)
    {
        GetResponse? getResponse = await _applicationRepository.GetAsync(id);
        if (getResponse == null)
        {
            throw new NoSuchApplicationException("Application doesn't exist");
        }
        return getResponse;
    }
    
    public async Task<GetResponse?> GetUncommitted(Guid id)
    {
        GetResponse? getResponse = await _applicationRepository.GetUncommitted(id);

        if (getResponse == null)
        {
            throw new NoSuchApplicationException("Application doesn't exist");
        }

        return getResponse;
    }

    public List<GetActivity> GetActivities()
    {
        List<GetActivity> list = new List<GetActivity>
        {
            new(ActivityType.Discussion, "Дискуссия / круглый стол, 40-50 минут"),
            new(ActivityType.Masterclass, "Мастеркласс, 1-2 часа"),
            new(ActivityType.Report, "Доклад, 35-45 минут")
        };
        return list;
    }
}
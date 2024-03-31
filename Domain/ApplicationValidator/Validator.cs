using Domain.Entity;
using Domain.Requests;
using Domain.Responses;

namespace DAL.ApplicationValidator;

public class Validator : IValidator
{
    public bool IsAtLeastOneFiled(AddRequest addRequest)
    {
        return addRequest.Description != null
               || addRequest.Outline != null
               || addRequest.ActivityType != null
               || addRequest.Name != null;
    }

    public bool IsAllFieldsOccured(GetResponse? getResponse)
    {
        return getResponse is { Outline: not null, ActivityType: not null, Name: not null };
    }

    public bool IsAtLeastOneFiled(UpdateRequest updateRequest)
    {
        return updateRequest.Description != null 
               || updateRequest.Outline != null
               || updateRequest.ActivityType != null
               || updateRequest.Name != null;
    }
}
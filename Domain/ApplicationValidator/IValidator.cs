using Domain.Entity;
using Domain.Requests;
using Domain.Responses;

namespace DAL.ApplicationValidator;

public interface IValidator
{
    bool IsAtLeastOneFiled(AddRequest addRequest);

    bool IsAllFieldsOccured(GetResponse? getResponse);
    bool IsAtLeastOneFiled(UpdateRequest updateRequest);
}
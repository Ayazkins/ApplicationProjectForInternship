using System.Net;

namespace Domain.Responses;

public abstract record Result
{
    public sealed record Success(GetResponse? GetResponse) : Result;

    public sealed record SuccessWithMoreObjects(IEnumerable<GetResponse> GetResponses) : Result;

    public sealed record Error(HttpStatusCode HttpStatusCode, string Description) : Result;
}
using Domain.Entity;

namespace Domain.Requests;

public record AddRequest(Guid Author,
    Activity? ActivityType,
    string? Name,
    string? Description,
    string? Outline);

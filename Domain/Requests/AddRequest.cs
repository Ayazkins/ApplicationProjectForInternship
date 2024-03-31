using Domain.Entity;

namespace Domain.Requests;

public record AddRequest(Guid Author,
    ActivityType? ActivityType,
    string? Name,
    string? Description,
    string? Outline);

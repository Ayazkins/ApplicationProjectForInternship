using Domain.Entity;

namespace Domain.Requests;

public record UpdateRequest(ActivityType? ActivityType,
    string? Name,
    string? Description,
    string? Outline);
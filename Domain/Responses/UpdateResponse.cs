using Domain.Entity;

namespace Domain.Responses;

public record UpdateResponse(Guid Id, 
    Guid Author,
    ActivityType? ActivityType,
    string? Name,
    string? Description,
    string? Outline);
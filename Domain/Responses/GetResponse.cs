using Domain.Entity;

namespace Domain.Responses;

public record GetResponse(Guid Id, 
    Guid Author,
    Activity? ActivityType,
    string? Name,
    string? Description,
    string? Outline);
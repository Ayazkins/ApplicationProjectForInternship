using Domain.Entity;

namespace Domain.Requests;

public record UpdateRequest(Activity? ActivityType,
    string? Name,
    string? Description,
    string? Outline);
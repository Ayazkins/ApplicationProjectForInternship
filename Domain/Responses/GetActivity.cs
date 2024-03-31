using Domain.Entity;

namespace Domain.Responses;

public record GetActivity(ActivityType ActivityType, string Message);
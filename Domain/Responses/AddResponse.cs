using System.Text.Json.Serialization;
using Domain.Entity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Domain.Responses;

public record AddResponse(Guid Id,
    Guid Author,
    ActivityType? ActivityType,
    string? Name,
    string? Description,
    string? Outline);
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entity;

public class Application : IEntityId<Guid>, IAuditable
{
    public Guid Id { get; set; }
    public Guid Author { get; set; }
    public ActivityType? ActivityType { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Outline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? SendAt { get; set; }
}
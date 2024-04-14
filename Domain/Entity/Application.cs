using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Requests;

namespace Domain.Entity;

public class Application 
{
    public Guid Id { get; set; }
    public Guid Author { get; private set; }
    public Activity? ActivityType { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? Outline { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? SendAt { get; private set;  }

    public Application CreateRequest(AddRequest addRequest)
    {
        Author = addRequest.Author;
        ActivityType = addRequest.ActivityType;
        Name = addRequest.Name;
        Description = addRequest.Description;
        Outline = addRequest.Outline;
        CreatedAt = DateTime.Now.ToUniversalTime();
        return this;
    }

    public Application UpdateRequest(UpdateRequest updateRequest)
    {
        ActivityType = updateRequest.ActivityType;
        Name = updateRequest.Name;
        Description = updateRequest.Description;
        Outline = updateRequest.Outline;
        UpdatedAt = DateTime.Now.ToUniversalTime();
        return this;
    }

    public Application Commit()
    {
        SendAt = DateTime.Now.ToUniversalTime();
        return this;
    }
}
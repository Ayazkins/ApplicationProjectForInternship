namespace Domain.Interfaces;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? SendAt { get; set; }
}
namespace DAL.Exceptions;

public class NotEnoughFieldsOccuredException : Exception
{
    public NotEnoughFieldsOccuredException() {}
    public NotEnoughFieldsOccuredException(string message) : base(message) {}
    public NotEnoughFieldsOccuredException(string message, Exception inner) : base(message, inner) {}
}
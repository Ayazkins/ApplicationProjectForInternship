namespace DAL.Exceptions;

public class NoSuchApplicationException : Exception
{
    public NoSuchApplicationException() {}
    public NoSuchApplicationException(string message) : base(message) {}
    public NoSuchApplicationException(string message, Exception inner) : base(message, inner) {}
}
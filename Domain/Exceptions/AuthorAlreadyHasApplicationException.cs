namespace DAL.Exceptions;

public class AuthorAlreadyHasApplicationException : Exception
{
    public AuthorAlreadyHasApplicationException() {}
    public AuthorAlreadyHasApplicationException(string message) : base(message) {}
    public AuthorAlreadyHasApplicationException(string message, Exception inner) : base(message, inner) {}
}
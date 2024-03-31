namespace DAL.Exceptions;

public class ApplicationAlreadyCommittedException : Exception
{
    public ApplicationAlreadyCommittedException() {}
    public ApplicationAlreadyCommittedException(string message) : base(message) {}
    public ApplicationAlreadyCommittedException(string message, Exception inner) : base(message, inner) {}
}
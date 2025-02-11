namespace Api.Exceptions;

public class ValidationFailException : Exception
{
    public ValidationFailException(string message) : base(message)
    { }
}
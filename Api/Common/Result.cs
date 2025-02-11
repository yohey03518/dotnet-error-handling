namespace Api.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public Exception Exception { get; }

    protected Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    private Result(bool isSuccess, string errorMessage, Exception exception)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error, Exception e) => new(false, error, e);
}

public class Result<T> : Result
{
    private readonly T _value;
    
    protected Result(T value, bool isSuccess, string errorMessage) 
        : base(isSuccess, errorMessage)
    {
        _value = value;
    }

    public T Value => IsSuccess 
        ? _value 
        : throw new InvalidOperationException("Cannot access value of a failed result.");

    public static Result<T> Success(T value) => new(value, true, string.Empty);
    public static Result<T> Failure(string error) => new(default!, false, error);
}
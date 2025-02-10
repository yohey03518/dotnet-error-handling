namespace Api;

public class BaseResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }

    public static BaseResponse Success(string? message = null)
    {
        return new BaseResponse
        {
            IsSuccess = true,
            Message = message
        };
    }
}

public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }

    public static BaseResponse<T> Success(T data, string? message = null)
    {
        return new BaseResponse<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }
}
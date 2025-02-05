namespace Api;

public class BaseResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}

public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }
}
namespace DEPI.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    public ErrorCode? ErrorCode { get; }

    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, T? value, string? error, ErrorCode? errorCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        ErrorCode = errorCode;
    }

    public static Result<T> Success(T value)
        => new(true, value, null, null);

    public static Result<T> Failure(string error, ErrorCode? errorCode = null)
        => new(false, default, error, errorCode);

    public object? ToMessageResponseList()
    {
        throw new NotImplementedException();
    }
}

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public ErrorCode? ErrorCode { get; }

    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, string? error, ErrorCode? errorCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorCode = errorCode;
    }

    public static Result Success()
        => new(true, null, null);

    public static Result Failure(string error, ErrorCode? errorCode = null)
        => new(false, error, errorCode);
}
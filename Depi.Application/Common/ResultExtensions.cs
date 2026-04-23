namespace DEPI.Application.Common;

public static class ResultExtensions
{
    public static Result<T> ToResult<T>(this T? value, string error, ErrorCode? errorCode = null)
        where T : class
    {
        if (value == null)
            return Result<T>.Failure(error, errorCode);

        return Result<T>.Success(value);
    }

    public static Result<TTo> Map<TFrom, TTo>(
        this Result<TFrom> result,
        Func<TFrom, TTo> map)
    {
        if (result.IsFailure)
            return Result<TTo>.Failure(result.Error!, result.ErrorCode);

        return Result<TTo>.Success(map(result.Value!));
    }

    public static async Task<Result<TTo>> MapAsync<TFrom, TTo>(
        this Result<TFrom> result,
        Func<TFrom, Task<TTo>> map)
    {
        if (result.IsFailure)
            return Result<TTo>.Failure(result.Error!, result.ErrorCode);

        return Result<TTo>.Success(await map(result.Value!));
    }

    public static Result<TTo> Bind<TFrom, TTo>(
        this Result<TFrom> result,
        Func<TFrom, Result<TTo>> func)
    {
        if (result.IsFailure)
            return Result<TTo>.Failure(result.Error!, result.ErrorCode);

        return func(result.Value!);
    }

    public static async Task<Result<TTo>> BindAsync<TFrom, TTo>(
        this Result<TFrom> result,
        Func<TFrom, Task<Result<TTo>>> func)
    {
        if (result.IsFailure)
            return Result<TTo>.Failure(result.Error!, result.ErrorCode);

        return await func(result.Value!);
    }

    public static Result<T> Ensure<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        string error,
        ErrorCode? errorCode = null)
    {
        if (result.IsFailure)
            return result;

        if (!predicate(result.Value!))
            return Result<T>.Failure(error, errorCode);

        return result;
    }
}
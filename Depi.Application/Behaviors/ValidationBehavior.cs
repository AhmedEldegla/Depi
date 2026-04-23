using DEPI.Application.Common;
using FluentValidation;
using MediatR;

namespace DEPI.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(validator =>
                validator.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (failures.Count != 0)
        {
            var errorMessages = failures
                .Select(f => f.ErrorMessage)
                .ToList();

            if (typeof(TResponse).IsGenericType)
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                if (resultType == typeof(Unit))
                {
                    return (TResponse)(object)Result.Failure(
                        string.Join("; ", errorMessages),
                        ErrorCode.ValidationError);
                }
                else
                {
                    var failureMethod = typeof(Result<>)
                        .MakeGenericType(resultType)
                        .GetMethod(nameof(Result<object>.Failure));

                    return (TResponse)failureMethod!.Invoke(null,
                        new object[] { string.Join("; ", errorMessages), ErrorCode.ValidationError })!;
                }
            }

            return (TResponse)(object)Result.Failure(
                string.Join("; ", errorMessages),
                ErrorCode.ValidationError);
        }

        return await next();
    }
}
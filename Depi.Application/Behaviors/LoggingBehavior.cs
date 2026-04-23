using MediatR;
using Microsoft.Extensions.Logging;

namespace DEPI.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation(
            "[DEPI] Handling {RequestName}",
            requestName);

        try
        {
            var response = await next();

            _logger.LogInformation(
                "[DEPI] Handled {RequestName} - Success",
                requestName);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "[DEPI] Error handling {RequestName}: {ErrorMessage}",
                requestName,
                ex.Message);

            throw;
        }
    }
}
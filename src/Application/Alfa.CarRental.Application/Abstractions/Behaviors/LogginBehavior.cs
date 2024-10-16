using MediatR;
using Alfa.CarRental.Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace Alfa.CarRental.Application.Abstractions.Behaviors;

public class LogginBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LogginBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string name = request.GetType().Name;

        try 
        { 
            _logger.LogInformation($"Executing request command: {name}");

            TResponse result = await next();

            _logger.LogInformation($"Request command {name} was executed successfully");

            return result;
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex, $"The request command {name} did not execute correctly");

            throw;
        }        
    }
}

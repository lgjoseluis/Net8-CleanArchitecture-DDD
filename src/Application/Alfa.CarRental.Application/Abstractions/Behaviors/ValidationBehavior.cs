using FluentValidation;
using MediatR;

using Alfa.CarRental.Application.Abstractions.Messaging;
using Alfa.CarRental.Application.Exceptions;

namespace Alfa.CarRental.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        { 
            return await next();
        }

        ValidationContext<TRequest> validationContext = new ValidationContext<TRequest>(request);

        List<ValidationError> validationErrors = _validators
            .Select(validators => validators.Validate(validationContext))
            .Where(result => result.Errors.Any())
            .SelectMany(result => result.Errors)
            .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}

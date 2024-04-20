using Auth.Domain.Errors;
using Auth.Domain.Shared;
using FluentValidation;
using Mediator;

namespace Auth.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
                                                    where TRequest : IRequest<TResponse>
                                                    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next(message, cancellationToken);
        }

        ValidationError[] errors = _validators
            .Select(validator => validator.Validate(message))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length != 0)
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next(message, cancellationToken);
    }

    private static TResult CreateValidationResult<TResult>(ValidationError[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (new ValidationResult(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object[] { errors })!;

        return (TResult)validationResult;
    }
}

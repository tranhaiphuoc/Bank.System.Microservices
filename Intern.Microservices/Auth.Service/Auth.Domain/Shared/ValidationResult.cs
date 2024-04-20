using Auth.Domain.Errors;

namespace Auth.Domain.Shared;

public sealed record ValidationResult(
    ValidationError[] Errors) : Result(false, ErrorMessage.BadRequest), IValidationResult
{
    public static ValidationResult WithErrors(ValidationError[] Errors) => new(Errors);
};

public sealed record ValidationResult<T>(
    ValidationError[] Errors) : Result<T>(false, default, ErrorMessage.BadRequest), IValidationResult
{
    public static ValidationResult<T> WithErrors(ValidationError[] Errors) => new(Errors);
};

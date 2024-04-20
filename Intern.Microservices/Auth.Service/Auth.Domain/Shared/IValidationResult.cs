using Auth.Domain.Errors;

namespace Auth.Domain.Shared;

public interface IValidationResult
{
    ValidationError[] Errors { get; }
}

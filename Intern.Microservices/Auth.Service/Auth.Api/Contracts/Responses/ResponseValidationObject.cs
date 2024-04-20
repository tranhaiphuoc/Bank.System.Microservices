using Auth.Domain.Errors;

namespace Auth.Api.Contracts.Responses;

public sealed record ResponseValidationObject(
    int Code,
    string Message,
    ValidationError[] Errors);


namespace Auth.Domain.Errors;

public sealed record ValidationError(string PropertyName, string Message);

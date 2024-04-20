using Auth.Domain.ValueObjects;

namespace Auth.Application.Contracts;

public sealed record LoginResult(
    string AccountNo,
    string IdCard,
    TokenPair Token);

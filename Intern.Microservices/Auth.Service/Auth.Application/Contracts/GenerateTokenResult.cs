using Auth.Domain.ValueObjects;

namespace Auth.Application.Contracts;

public sealed record GenerateTokenResult(
    string JwtId,
    DateTime RefreshTokenIssuedAt,
    DateTime RefreshTokenExpiredAt,
    TokenPair Token);

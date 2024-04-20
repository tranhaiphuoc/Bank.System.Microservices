namespace Auth.Domain.ValueObjects;

public sealed record TokenPair(
    string AccessToken,
    DateTime ExpiryDate,
    string RefreshToken);

namespace Auth.Domain.ValueObjects;

public sealed record TokenSetting(
    string Secret,
    string Issuer,
    string Audience,
    int AccessExpiryMinutes,
    int RefreshExpiryDays);

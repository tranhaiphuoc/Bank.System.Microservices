namespace Auth.Domain.Enitties;

public sealed record UserCredential(
    string Username,
    string PasswordHash,
    string AccountNumber,
    string RoleName);

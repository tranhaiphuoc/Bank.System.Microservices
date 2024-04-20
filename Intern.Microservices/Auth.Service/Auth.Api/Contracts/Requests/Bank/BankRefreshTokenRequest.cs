namespace Auth.Api.Contracts.Requests.Bank;

public sealed record BankRefreshTokenRequest(string AccessToken, string RefreshToken);

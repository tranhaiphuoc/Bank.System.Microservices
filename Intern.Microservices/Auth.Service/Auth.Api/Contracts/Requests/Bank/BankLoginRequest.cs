namespace Auth.Api.Contracts.Requests.Bank;

public sealed record BankLoginRequest(string Username, string Password);
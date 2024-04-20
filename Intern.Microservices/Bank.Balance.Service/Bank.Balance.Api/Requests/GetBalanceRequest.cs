namespace Bank.Balance.Api.Requests;

public sealed record GetBalanceRequest(string IdCard, string AccountNumber);

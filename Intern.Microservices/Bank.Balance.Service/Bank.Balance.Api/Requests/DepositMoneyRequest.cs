namespace Bank.Balance.Api.Requests;

public sealed record DepositMoneyRequest(string AccountNumber, decimal Amount, string Description);

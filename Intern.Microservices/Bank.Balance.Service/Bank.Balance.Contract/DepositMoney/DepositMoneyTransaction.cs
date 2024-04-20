namespace Bank.Balance.Contract.DepositMoney;

public sealed record DepositMoneyTransaction(string AccountNumber, decimal Amount, string Description);

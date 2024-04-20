namespace Bank.Transaction.Domain.ValueObjects;

public sealed record DepositTransactionDetail(string AccountNumber, decimal Amount);

namespace Bank.Transaction.Domain.ValueObjects;

public sealed record PaymentTransactionDetail(string AccountNumber, sbyte Direction, decimal Amount);

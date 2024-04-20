namespace Bank.Transaction.Domain.ValueObjects;

public sealed class TransactionDetail
{
    public required string AccountNumber { get; set; }

    public sbyte Direction { get; set; }

    public decimal Amount { get; set; }
}

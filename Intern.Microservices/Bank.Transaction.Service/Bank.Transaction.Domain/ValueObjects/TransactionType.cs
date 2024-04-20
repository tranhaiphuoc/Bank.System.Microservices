namespace Bank.Transaction.Domain.ValueObjects;

public class TransactionType
{
    public sbyte Id { get; set; }

    public string Name { get; set; } = null!;
}

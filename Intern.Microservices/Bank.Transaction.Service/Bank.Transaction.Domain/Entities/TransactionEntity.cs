using Bank.Transaction.Domain.ValueObjects;

namespace Bank.Transaction.Domain.Entities;

public class TransactionEntity : IAggregateRoot
{
    public Guid TransactionId { get; set; }

    public DateTime TransactionDate { get; set; }

    public string Description { get; set; } = null!;

    public sbyte Status { get; set; }

    public string ApprovedBy { get; set; } = null!;

    public TransactionType Type { get; set; } = null!;

    public List<TransactionDetail> TransactionDetails { get; set; } = [];
}

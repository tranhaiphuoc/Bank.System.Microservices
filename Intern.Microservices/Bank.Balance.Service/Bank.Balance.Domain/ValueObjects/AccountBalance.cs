namespace Bank.Balance.Domain.ValueObjects;

public sealed record AccountBalance(decimal UsableAmount, decimal HoldAmount, decimal TotalAmount);

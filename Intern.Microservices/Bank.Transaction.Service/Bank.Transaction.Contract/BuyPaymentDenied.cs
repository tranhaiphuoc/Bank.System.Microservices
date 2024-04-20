namespace Bank.Transaction.Contract;

public sealed record BuyPaymentDenied(string AccountNumber, string SecuritiesAccount, decimal Amount);

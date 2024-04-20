namespace Bank.Transaction.Contract;

public sealed record BuyPaymentApproved(string AccountNumber, string SecuritiesAccount, decimal Amount);
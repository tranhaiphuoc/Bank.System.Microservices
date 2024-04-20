using Bank.Transaction.Domain.Entities;
using Bank.Transaction.Domain.ValueObjects;

namespace Bank.Transaction.Application.Abstraction.Persistence;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionEntity>> GetApproved();

    Task<IEnumerable<TransactionEntity>> GetUnapproved();

    Task<DepositTransactionDetail?> GetDepositTransactionDetails(
        string transactionId);

    Task<IEnumerable<PaymentTransactionDetail>> GetBuyPaymentTransactionDetails(
        string transactionId);

    Task<IEnumerable<PaymentTransactionDetail>> GetSellPaymentTransactionDetails(
        string transactionId);

    Task<ErrorCode> DepositMoney(
        string accountNumber,
        decimal amount,
        string description);

    Task<ErrorCode> DepositApproval(
        string transactionId,
        decimal status,
        string approvedBy);

    Task<ErrorCode> BuyPayment(
        string accountNumber,
        string securitiesAccount,
        decimal amount,
        string description);

    Task<ErrorCode> BuyPaymentApproval(
        string transactionId,
        decimal status,
        string approvedBy);

    Task<ErrorCode> SellPayment(
        string accountNumber,
        string securitiesAccount,
        decimal amount,
        string description);

    Task<ErrorCode> SellPaymentApproval(
        string transactionId,
        decimal status,
        string approvedBy);
}

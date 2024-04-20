using Bank.Balance.Domain.ValueObjects;

namespace Bank.Balance.Application.Abstraction.Persistence;

public interface IBalanceRepository
{
    Task<ErrorCode> IsBalanceExist(string accountNumber);

    Task<ErrorCode> IsBalanceValid(string idCard, string accountNumber);

    Task<AccountBalance?> GetBalance(string idCard, string accountNumber);

    Task AddBalance(string idCard, string accountNumber);

    Task<ErrorCode> HoldAmount(string idCard, string accountNumber, decimal amount);

    Task<ErrorCode> UnholdAmount(string idCard, string accountNumber, decimal amount);

    Task<ErrorCode> BuyPayment(
        string idCard,
        string accountNumber,
        string securitiesAccountIdCard,
        string securitiesAccount,
        decimal amount);

    Task<ErrorCode> SellPayment(
        string idCard,
        string accountNumber,
        string securitiesAccountIdCard,
        string securitiesAccount,
        decimal amount);

    Task<ErrorCode> DepositMoneyApproved(string accountNumber, decimal amount);

    Task<ErrorCode> BuyPaymentApproved(
        string accountNumber,
        string securitiesAccount,
        decimal amount);

    Task<ErrorCode> BuyPaymentDenied(
        string accountNumber,
        string securitiesAccount,
        decimal amount);

    Task<ErrorCode> SellPaymentApproved(
        string accountNumber,
        string securitiesAccount,
        decimal amount);

    Task<ErrorCode> SellPaymentDenied(
        string accountNumber,
        string securitiesAccount,
        decimal amount);
}

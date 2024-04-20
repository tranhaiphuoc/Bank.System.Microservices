using Bank.Balance.Application.Abstraction.Data;
using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Domain.Shared;
using Bank.Balance.Domain.ValueObjects;

namespace Bank.Balance.Infrastructure.Persistence;

internal class BalanceRepository(IDataAccess dataAccess) : IBalanceRepository
{
    private readonly IDataAccess _dataAccess = dataAccess;

    public async Task<ErrorCode> IsBalanceExist(string accountNumber)
    {
        var parameters = new { accountNumber };

        var code = await _dataAccess
            .LoadDataAsync<int, dynamic>(StoredProcedure.IsBalanceExist, parameters);

        return new(code.First());
    }

    public async Task<ErrorCode> IsBalanceValid(string idCard, string accountNumber)
    {
        var parameters = new { idCard, accountNumber };

        var code = await _dataAccess
            .LoadDataAsync<int, dynamic>(StoredProcedure.IsBalanceValid, parameters);

        return new(code.First());
    }

    public async Task<AccountBalance?> GetBalance(string idCard, string accountNumber)
    {
        var parameters = new { idCard, accountNumber };

        var result = await _dataAccess.LoadDataAsync<AccountBalance, dynamic>(
            StoredProcedure.GetBalance,
            parameters);

        return result.FirstOrDefault();
    }

    public async Task AddBalance(string idCard, string accountNumber)
    {
        var parameters = new { idCard, accountNumber };

        await _dataAccess.SaveDataAsync(StoredProcedure.AddBalance, parameters);
    }

    public async Task<ErrorCode> HoldAmount(
        string idCard,
        string accountNumber,
        decimal amount)
    {
        var parameters = new { idCard, accountNumber, amount };

        var result = await _dataAccess
            .LoadDataAsync<int, dynamic>(StoredProcedure.HoldAmount, parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> UnholdAmount(
        string idCard,
        string accountNumber,
        decimal amount)
    {
        var parameters = new { idCard, accountNumber, amount };

        var result = await _dataAccess
            .LoadDataAsync<int, dynamic>(StoredProcedure.UnholdAmount, parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> BuyPayment(
        string idCard,
        string accountNumber,
        string securitiesAccountIdCard,
        string securitiesAccount,
        decimal amount)
    {
        var parameters = new
        {
            idCard,
            accountNumber,
            securitiesAccountIdCard,
            securitiesAccount,
            amount
        };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.BuyPayment,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> SellPayment(
        string idCard,
        string accountNumber,
        string securitiesAccountIdCard,
        string securitiesAccount,
        decimal amount)
    {
        var parameters = new
        {
            idCard,
            accountNumber,
            securitiesAccountIdCard,
            securitiesAccount,
            amount
        };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.SellPayment,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> DepositMoneyApproved(string accountNumber, decimal amount)
    {
        var parameters = new { accountNumber, amount };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.DepositMoneyApproved,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> BuyPaymentApproved(
        string accountNumber,
        string securitiesAccount,
        decimal amount)
    {
        var parameters = new { accountNumber, securitiesAccount, amount };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.BuyPaymentApproved,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> BuyPaymentDenied(
        string accountNumber,
        string securitiesAccount,
        decimal amount)
    {
        var parameters = new { accountNumber, securitiesAccount, amount };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.BuyPaymentDenied,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> SellPaymentApproved(
        string accountNumber,
        string securitiesAccount,
        decimal amount)
    {
        var parameters = new { accountNumber, securitiesAccount, amount };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.SellPaymentApproved,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> SellPaymentDenied(
        string accountNumber,
        string securitiesAccount,
        decimal amount)
    {
        var parameters = new { accountNumber, securitiesAccount, amount };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.SellPaymentDenied,
            parameters);

        return new(result.First());
    }
}

using Bank.Transaction.Application.Abstraction.Data;
using Bank.Transaction.Application.Abstraction.Persistence;
using Bank.Transaction.Domain.Entities;
using Bank.Transaction.Domain.Shared;
using Bank.Transaction.Domain.ValueObjects;
using Dapper;
using MySql.Data.MySqlClient;

namespace Bank.Transaction.Infrastructure.Persistence;

internal class TransactionRepository(IDataAccess dataAccess) : ITransactionRepository
{
    private readonly IDataAccess _dataAccess = dataAccess;

    public async Task<IEnumerable<TransactionEntity>> GetApproved()
    {
        using var connection = new MySqlConnection(_dataAccess.GetConnectionString());

        var transactions = await connection
            .QueryAsync<TransactionEntity, TransactionType, TransactionDetail, TransactionEntity>(
            StoredProcedure.GetApprovedTransaction,
            (entity, type, detail) =>
            {
                entity.Type = type;
                entity.TransactionDetails.Add(detail);
                return entity;
            },
            splitOn: "id,accountNumber");

        var result = transactions.GroupBy(ts => ts.TransactionId).Select(g =>
        {
            var groupedTransaction = g.First();
            groupedTransaction.TransactionDetails = g.Select(ts => ts.TransactionDetails.Single()).ToList();
            return groupedTransaction;
        });

        return result;
    }

    public async Task<IEnumerable<TransactionEntity>> GetUnapproved()
    {
        using var connection = new MySqlConnection(_dataAccess.GetConnectionString());

        var transactions = await connection
            .QueryAsync<TransactionEntity, TransactionType, TransactionDetail, TransactionEntity>(
            StoredProcedure.GetUnaprovedTransaction,
            (entity, type, detail) =>
            {
                entity.Type = type;
                entity.TransactionDetails.Add(detail);
                return entity;
            },
            splitOn: "id,accountNumber");

        var result = transactions.GroupBy(ts => ts.TransactionId).Select(g =>
        {
            var groupedTransaction = g.First();
            groupedTransaction.TransactionDetails = g.Select(ts => ts.TransactionDetails.Single()).ToList();
            return groupedTransaction;
        });

        return result;
    }

    public async Task<DepositTransactionDetail?> GetDepositTransactionDetails(
        string transactionId)
    {
        var result = await _dataAccess.LoadDataAsync<DepositTransactionDetail, dynamic>(
            StoredProcedure.GetDepositTransactionDetails,
            new { transactionId });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<PaymentTransactionDetail>> GetBuyPaymentTransactionDetails(
        string transactionId)
    {
        var result = await _dataAccess.LoadDataAsync<PaymentTransactionDetail, dynamic>(
            StoredProcedure.GetBuyPaymentTransactionDetails,
            new { transactionId });

        return result;
    }

    public async Task<IEnumerable<PaymentTransactionDetail>> GetSellPaymentTransactionDetails(
        string transactionId)
    {
        var result = await _dataAccess.LoadDataAsync<PaymentTransactionDetail, dynamic>(
            StoredProcedure.GetSellPaymentTransactionDetails,
            new { transactionId });

        return result;
    }

    public async Task<ErrorCode> DepositMoney(
        string accountNumber,
        decimal amount,
        string description)
    {
        var parameters = new { accountNumber, amount, description };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.DepositMoneyTransaction,
            parameters);

        return new(result.First());
    }
    
    public async Task<ErrorCode> DepositApproval(
        string transactionId,
        decimal status,
        string approvedBy)
    {
        var parameters = new { transactionId, status, approvedBy };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.DepositMoneyApproval,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> BuyPayment(
        string accountNumber,
        string securitiesAccount,
        decimal amount,
        string description)
    {
        var parameters = new { accountNumber, securitiesAccount, amount, description };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.BuyPaymentTransaction,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> BuyPaymentApproval(
        string transactionId,
        decimal status,
        string approvedBy)
    {
        var parameters = new { transactionId, status, approvedBy };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.BuyPaymentApproval,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> SellPayment(
        string accountNumber,
        string securitiesAccount,
        decimal amount,
        string description)
    {
        var parameters = new { accountNumber, securitiesAccount, amount, description };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.SellPaymentTransaction,
            parameters);

        return new(result.First());
    }

    public async Task<ErrorCode> SellPaymentApproval(
        string transactionId,
        decimal status,
        string approvedBy)
    {
        var parameters = new { transactionId, status, approvedBy };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.SellPaymentApproval,
            parameters);

        return new(result.First());
    }
}

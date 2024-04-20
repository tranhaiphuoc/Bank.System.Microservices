using Bank.Customer.Application.Abstraction.Data;
using Bank.Customer.Application.Abstraction.Persistence;
using Bank.Customer.Domain.Shared;
using Bank.Customer.Domain.ValueObjects;

namespace Bank.Customer.Infrastructure.Persistence;

public class CustomerRepository(IDataAccess dataAccess) : ICustomerRepository
{
    private readonly IDataAccess _dataAccess = dataAccess;
    
    public async Task<ErrorCode> IsCustomerAccountValid(
        string accountNumber,
        string idCard)
    {
        var parameters = new { accountNumber, idCard };

        var result = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.IsCustomerAccountValid,
            parameters);

        return new(result.First());
    }

    public async Task<CustomerAccount?> GetCustomerAccount(
        string accountNumber,
        string idCard)
    {
        var parameters = new { accountNumber, idCard };

        var result = await _dataAccess.LoadDataAsync<CustomerAccount, dynamic>(
            StoredProcedure.GetCustomerAccount,
            parameters);

        return result.FirstOrDefault();
    }

    public async Task<ErrorCode> OpenCustomer(
        string idCard,
        string name,
        DateTime dateOfBirth,
        string phoneNumber,
        string address,
        DateTime idCardDate,
        string idCardPlace,
        string accountNumber,
        int accountType,
        string passwordHash)
    {
        var parameters = new
        {
            idCard,
            name,
            dateOfBirth,
            phoneNumber,
            address,
            idCardDate,
            idCardPlace,
            accountNumber
        };

        var errorCode = await _dataAccess
            .LoadDataAsync<int, dynamic>(StoredProcedure.OpenCustomer, parameters);

        return new(errorCode.First());
    }
}

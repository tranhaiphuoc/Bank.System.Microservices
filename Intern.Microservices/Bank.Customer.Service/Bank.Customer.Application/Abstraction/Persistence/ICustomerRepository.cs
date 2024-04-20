using Bank.Customer.Domain.ValueObjects;

namespace Bank.Customer.Application.Abstraction.Persistence;

public interface ICustomerRepository
{
    Task<ErrorCode> IsCustomerAccountValid(
        string accountNumber,
        string idCard);

    Task<CustomerAccount?> GetCustomerAccount(
        string accountNumber,
        string idCard);

    Task<ErrorCode> OpenCustomer(
        string idCard,
        string name,
        DateTime dateOfBirth,
        string phoneNumber,
        string address,
        DateTime idCardDate,
        string idCardPlace,
        string accountNumber,
        int accountType,
        string passwordHash);
}

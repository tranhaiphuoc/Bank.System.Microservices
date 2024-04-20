using Bank.Account.Domain.ValueObjects;

namespace Bank.Account.Application.Abstraction.Persistence;

public interface IAccountRepository
{
    Task<ErrorCode> CreateAccountAsync(
        string idCard, string accountNumber, int typeId);
}

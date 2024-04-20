using Bank.Account.Application.Abstraction.Data;
using Bank.Account.Application.Abstraction.Persistence;
using Bank.Account.Domain.Shared;
using Bank.Account.Domain.ValueObjects;

namespace Bank.Account.Infrastructure.Persistence;

public class AccountRepository(IDataAccess dataAccess) : IAccountRepository
{
    private readonly IDataAccess _dataAccess = dataAccess;

    public async Task<ErrorCode> CreateAccountAsync(
        string idCard,
        string accountNumber,
        int typeId)
    {
        var parameters = new { idCard, accountNumber, typeId };

        var errorCode = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.CreateAccount,
            parameters);

        return new(errorCode.First());
    }
}

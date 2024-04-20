using Bank.Balance.Domain.Shared;

namespace Bank.Balance.Application.Abstraction.Services;

public interface IHelper
{
    ValueTask<Result> GetError(int code);

    ValueTask<Result<T>> GetError<T>(int code, T value);
}

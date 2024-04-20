using Bank.Transaction.Domain.Shared;

namespace Bank.Transaction.Application.Abstraction.Services;

public interface IHelper
{
    ValueTask<Result> GetError(int code);

    ValueTask<Result<T>> GetError<T>(int code, T value);
}

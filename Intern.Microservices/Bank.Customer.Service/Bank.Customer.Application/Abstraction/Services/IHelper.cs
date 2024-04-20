using Bank.Customer.Domain.Shared;

namespace Bank.Customer.Application.Abstraction.Services;

public interface IHelper
{
    ValueTask<Result> GetError(int code);

    ValueTask<Result<T>> GetError<T>(int code, T value);

    ValueTask<string> HashPassword(string password);
}

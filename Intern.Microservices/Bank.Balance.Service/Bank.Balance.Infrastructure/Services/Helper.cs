using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Domain.Errors;
using Bank.Balance.Domain.Shared;

namespace Bank.Customer.Infrastructure.Services;

internal class Helper : IHelper
{
    public ValueTask<Result> GetError(int code)
    {
        Result result = code switch
        {
            1 => Result.Failure(ErrorMessage.NotFound),
            2 => Result.Failure(ErrorMessage.Exception),

            10 => Result.Failure(ErrorMessage.IdCardNotBelongToAccount),
            11 => Result.Failure(ErrorMessage.AccountNotHaveBalance),
            12 => Result.Failure(ErrorMessage.TakenAmountExceedBalance),
            13 => Result.Failure(ErrorMessage.HoldAmountExceedUsable),
            14 => Result.Failure(ErrorMessage.UnholdAmountExceedHold),
            15 => Result.Failure(ErrorMessage.AccountNumberUsed),
            16 => Result.Failure(ErrorMessage.IdCardUsed),
            17 => Result.Failure(ErrorMessage.InvalidCredential),

            19 => Result.Failure(ErrorMessage.Unauthorized),
            20 => Result.Failure(ErrorMessage.TokenNotExpired),
            _ => Result.Success()
        };

        return ValueTask.FromResult(result);
    }

    public ValueTask<Result<T>> GetError<T>(int code, T value)
    {
        Result<T> result = code switch
        {
            1 => Result<T>.Failure(ErrorMessage.NotFound),
            2 => Result<T>.Failure(ErrorMessage.Exception),

            10 => Result<T>.Failure(ErrorMessage.IdCardNotBelongToAccount),
            11 => Result<T>.Failure(ErrorMessage.AccountNotHaveBalance),
            12 => Result<T>.Failure(ErrorMessage.TakenAmountExceedBalance),
            13 => Result<T>.Failure(ErrorMessage.HoldAmountExceedUsable),
            14 => Result<T>.Failure(ErrorMessage.UnholdAmountExceedHold),
            15 => Result<T>.Failure(ErrorMessage.AccountNumberUsed),
            16 => Result<T>.Failure(ErrorMessage.IdCardUsed),
            17 => Result<T>.Failure(ErrorMessage.InvalidCredential),

            19 => Result<T>.Failure(ErrorMessage.Unauthorized),
            20 => Result<T>.Failure(ErrorMessage.TokenNotExpired),
            _ => Result<T>.Success(value)
        };

        return ValueTask.FromResult(result);
    }
}

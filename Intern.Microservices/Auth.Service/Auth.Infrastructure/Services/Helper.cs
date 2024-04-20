using Auth.Application.Abstraction.Services;
using Auth.Domain.Errors;

namespace Auth.Infrastructure.Services;

public sealed class Helper : IHelper
{
    public Task<Error> GetError(int code)
    {
        Error error = code switch
        {
            1 => ErrorMessage.NotFound,
            10 => ErrorMessage.IdCardNotBelongToAccount,
            11 => ErrorMessage.AccountNotHaveBalance,
            12 => ErrorMessage.TakenAmountExceedBalance,
            13 => ErrorMessage.HoldAmountExceedUsable,
            14 => ErrorMessage.UnholdAmountExceedHold,
            15 => ErrorMessage.AccountNumberUsed,
            16 => ErrorMessage.IdCardUsed,
            17 => ErrorMessage.InvalidCredential,

            19 => ErrorMessage.Unauthorized,
            20 => ErrorMessage.TokenNotExpired,
            _ => ErrorMessage.Exception
        };

        return Task.FromResult(error);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPasswordHash(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}

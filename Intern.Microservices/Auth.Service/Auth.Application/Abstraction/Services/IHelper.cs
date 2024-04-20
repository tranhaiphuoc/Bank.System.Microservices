using Auth.Domain.Errors;

namespace Auth.Application.Abstraction.Services;

public interface IHelper
{
    Task<Error> GetError(int code);

    string HashPassword(string password);

    bool VerifyPasswordHash(string password, string passwordHash);
}

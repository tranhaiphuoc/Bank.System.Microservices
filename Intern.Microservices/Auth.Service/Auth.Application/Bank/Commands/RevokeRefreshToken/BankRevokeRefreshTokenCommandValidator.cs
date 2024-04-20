using FluentValidation;

namespace Auth.Application.Bank.Commands.RevokeRefreshToken;

internal class BankRevokeRefreshTokenCommandValidator : AbstractValidator<BankRevokeRefreshTokenCommand>
{
    public BankRevokeRefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}

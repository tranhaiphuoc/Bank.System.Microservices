using FluentValidation;

namespace Auth.Application.Bank.Commands.RefreshToken;

internal class BankRefreshTokenCommandValidator : AbstractValidator<BankRefreshTokenCommand>
{
    public BankRefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}

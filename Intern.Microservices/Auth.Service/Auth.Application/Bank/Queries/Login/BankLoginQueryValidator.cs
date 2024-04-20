using FluentValidation;

namespace Auth.Application.Bank.Queries.Login;

internal class BankLoginQueryValidator : AbstractValidator<BankLoginQuery>
{
    public BankLoginQueryValidator()
    {
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}

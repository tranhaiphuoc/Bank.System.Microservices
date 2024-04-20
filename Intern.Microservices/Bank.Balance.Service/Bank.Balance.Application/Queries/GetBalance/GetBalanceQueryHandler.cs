using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Domain.Errors;
using Bank.Balance.Domain.Shared;
using Bank.Balance.Domain.ValueObjects;
using Mediator;

namespace Bank.Balance.Application.Queries.GetBalance;

internal class GetBalanceQueryHandler(
    IBalanceRepository balanceRepository) : IRequestHandler<GetBalanceQuery, Result<AccountBalance>>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;

    public async ValueTask<Result<AccountBalance>> Handle(
        GetBalanceQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _balanceRepository.GetBalance(request.IdCard, request.AccountNumber);

        if (result is null)
        {
            return Result<AccountBalance>.Failure(ErrorMessage.NotFound);
        }

        return Result<AccountBalance>.Success(result);
    }
}

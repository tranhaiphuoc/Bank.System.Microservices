using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Domain.Shared;
using Mediator;

namespace Bank.Balance.Application.Queries.IsBalanceValid;

internal class IsBalanceValidQueryHandler(
    IBalanceRepository balanceRepository,
    IHelper helper) : IRequestHandler<IsBalanceValidQuery, Result>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;
    private readonly IHelper _helper = helper;

    public async ValueTask<Result> Handle(
        IsBalanceValidQuery request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _balanceRepository
            .IsBalanceValid(request.IdCard, request.AccountNumber);

        return await _helper.GetError(errorCode.Code);
    }
}

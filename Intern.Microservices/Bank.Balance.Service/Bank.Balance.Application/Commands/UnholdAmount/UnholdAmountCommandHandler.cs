using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Domain.Shared;
using Mediator;

namespace Bank.Balance.Application.Commands.UnholdAmount;

internal class UnholdAmountCommandHandler(
    IBalanceRepository balanceRepository,
    IHelper helper) : IRequestHandler<UnholdAmountCommand, Result>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;
    private readonly IHelper _helper = helper;

    public async ValueTask<Result> Handle(
        UnholdAmountCommand request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _balanceRepository
            .UnholdAmount(request.IdCard, request.AccountNumber, request.Amount);

        var result = await _helper.GetError(errorCode.Code);

        return result;
    }
}

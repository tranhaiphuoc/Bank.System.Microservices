using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Domain.Shared;
using Mediator;

namespace Bank.Balance.Application.Commands.HoldAmount;

internal class HoldAmountCommandHandler(
    IBalanceRepository balanceRepository,
    IHelper helper) : IRequestHandler<HoldAmountCommand, Result>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;
    private readonly IHelper _helper = helper;

    public async ValueTask<Result> Handle(HoldAmountCommand request, CancellationToken cancellationToken)
    {
        var errorCode = await _balanceRepository
            .HoldAmount(request.IdCard, request.AccountNumber, request.Amount);

        var result = await _helper.GetError(errorCode.Code);

        return result;
    }
}

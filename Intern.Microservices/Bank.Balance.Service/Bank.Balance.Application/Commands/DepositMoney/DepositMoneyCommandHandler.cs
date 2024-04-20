using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Contract.DepositMoney;
using Bank.Balance.Domain.Shared;
using MassTransit;
using Mediator;

namespace Bank.Balance.Application.Commands.DepositMoney;

internal class DepositMoneyCommandHandler(
    IBalanceRepository balanceRepository,
    IHelper helper,
    IPublishEndpoint publishEndpoint) : IRequestHandler<DepositMoneyCommand, Result>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;
    private readonly IHelper _helper = helper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async ValueTask<Result> Handle(
        DepositMoneyCommand request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _balanceRepository.IsBalanceExist(request.AccountNumber);

        var result = await _helper.GetError(errorCode.Code);

        if (result.IsSuccess)
        {
            await _publishEndpoint.Publish<DepositMoneyTransaction>(
                new(request.AccountNumber, request.Amount, request.Description),
                cancellationToken);
        }

        return result;
    }
}

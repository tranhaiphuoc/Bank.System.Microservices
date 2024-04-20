using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Contract.SellPayment;
using Bank.Balance.Domain.Shared;
using Bank.Balance.Domain.ValueObjects;
using MassTransit;
using Mediator;

namespace Bank.Balance.Application.Commands.SellPayment;

internal class SellPaymentCommandHandler(
    IBalanceRepository balanceRepository,
    IHelper helper,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SellPaymentCommand, Result<AccountBalance>>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;
    private readonly IHelper _helper = helper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async ValueTask<Result<AccountBalance>> Handle(
        SellPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _balanceRepository.SellPayment(
            request.IdCard,
            request.AccountNumber,
            request.SecuritiesIdCard,
            request.SecuritiesAccount,
            request.Amount);

        var result = await _helper.GetError(errorCode.Code);

        if (result.IsSuccess)
        {
            await _publishEndpoint.Publish<SellPaymentTransaction>(
                new(request.AccountNumber, request.SecuritiesAccount, request.Amount, request.Description),
                cancellationToken);

            var balance = await _balanceRepository.GetBalance(request.IdCard, request.AccountNumber);

            return Result<AccountBalance>.Success(balance!);
        }

        return Result<AccountBalance>.Failure(result.Error);
    }
}

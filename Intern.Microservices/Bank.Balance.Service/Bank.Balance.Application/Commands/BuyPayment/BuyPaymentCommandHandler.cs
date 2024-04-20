using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Contract.BuyPayment;
using Bank.Balance.Domain.Shared;
using Bank.Balance.Domain.ValueObjects;
using MassTransit;
using Mediator;

namespace Bank.Balance.Application.Commands.BuyPayment;

internal class BuyPaymentCommandHandler(
    IBalanceRepository balanceRepository,
    IHelper helper,
    IPublishEndpoint publishEndpoint) : IRequestHandler<BuyPaymentCommand, Result<AccountBalance>>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;
    private readonly IHelper _helper = helper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async ValueTask<Result<AccountBalance>> Handle(
        BuyPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _balanceRepository.BuyPayment(
            request.IdCard,
            request.AccountNumber,
            request.SecuritiesIdCard,
            request.SecuritiesAccount,
            request.Amount);

        var result = await _helper.GetError(errorCode.Code);

        if (result.IsSuccess)
        {
            await _publishEndpoint.Publish<BuyPaymentTransaction>(
                new(request.AccountNumber, request.SecuritiesAccount, request.Amount, request.Description),
                cancellationToken);

            var balance = await _balanceRepository.GetBalance(
                request.IdCard,
                request.AccountNumber);

            return Result<AccountBalance>.Success(balance!);
        }

        return Result<AccountBalance>.Failure(result.Error);
    }
}

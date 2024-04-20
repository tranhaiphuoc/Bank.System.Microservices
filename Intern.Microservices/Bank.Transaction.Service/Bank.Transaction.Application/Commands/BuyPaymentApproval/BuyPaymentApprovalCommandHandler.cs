using Bank.Transaction.Application.Abstraction.Persistence;
using Bank.Transaction.Application.Abstraction.Services;
using Bank.Transaction.Contract;
using Bank.Transaction.Domain.Shared;
using MassTransit;
using Mediator;

namespace Bank.Transaction.Application.Commands.BuyPaymentApproval;

internal class BuyPaymentApprovalCommandHandler(
    ITransactionRepository transactionRepository,
    IHelper helper,
    IPublishEndpoint publishEndpoint) : IRequestHandler<BuyPaymentApprovalCommand, Result>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IHelper _helper = helper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async ValueTask<Result> Handle(
        BuyPaymentApprovalCommand request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _transactionRepository.BuyPaymentApproval(
            request.TransactionId,
            request.Status,
            request.ApprovedBy);

        var result = await _helper.GetError(errorCode.Code);

        if (result.IsSuccess)
        {
            var details = (await _transactionRepository
                .GetBuyPaymentTransactionDetails(request.TransactionId)).ToList();

            if (details.Count == 2)
            {
                var accountNumber = 0;
                var securitiesAccount = 1;

                if (details[0].Direction == 1)
                {
                    accountNumber = 1;
                    securitiesAccount = 0;
                }

                switch (request.Status)
                {
                    case 1:
                        await _publishEndpoint.Publish<BuyPaymentApproved>(
                            new(
                                details[accountNumber].AccountNumber,
                                details[securitiesAccount].AccountNumber,
                                details[0].Amount),
                            cancellationToken);
                        break;

                    case -1:
                        await _publishEndpoint.Publish<BuyPaymentDenied>(
                            new(
                                details[accountNumber].AccountNumber,
                                details[securitiesAccount].AccountNumber,
                                details[0].Amount),
                            cancellationToken);
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        return result;
    }
}

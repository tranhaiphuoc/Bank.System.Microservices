using Bank.Transaction.Application.Abstraction.Persistence;
using Bank.Transaction.Application.Abstraction.Services;
using Bank.Transaction.Contract;
using Bank.Transaction.Domain.Shared;
using MassTransit;
using Mediator;

namespace Bank.Transaction.Application.Commands.DepositApproval;

internal class DepositApprovalCommandHandler(
    ITransactionRepository transactionRepository,
    IHelper helper,
    IPublishEndpoint publishEndpoint) : IRequestHandler<DepositApprovalCommand, Result>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IHelper _helper = helper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async ValueTask<Result> Handle(
        DepositApprovalCommand request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _transactionRepository.DepositApproval(
            request.TransactionId,
            request.Status,
            request.ApprovedBy);

        var result = await _helper.GetError(errorCode.Code);

        if (result.IsSuccess)
        {
            var details = await _transactionRepository
                .GetDepositTransactionDetails(request.TransactionId);

            if (details is not null)
            {
                switch (request.Status)
                {
                    case 1:
                        await _publishEndpoint.Publish<DepositMoneyApproved>(
                            new(details.AccountNumber, details.Amount),
                            cancellationToken);
                        break;

                    case -1:
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        return result;
    }
}

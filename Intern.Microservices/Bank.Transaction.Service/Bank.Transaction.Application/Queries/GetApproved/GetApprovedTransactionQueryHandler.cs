using Bank.Transaction.Application.Abstraction.Persistence;
using Bank.Transaction.Domain.Entities;
using Bank.Transaction.Domain.Shared;
using Mediator;

namespace Bank.Transaction.Application.Queries.GetApproved;

internal class GetApprovedTransactionQueryHandler(
    ITransactionRepository transactionRepository) : 
    IRequestHandler<GetApprovedTransactionQuery, Result<IEnumerable<TransactionEntity>>>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async ValueTask<Result<IEnumerable<TransactionEntity>>> Handle(
        GetApprovedTransactionQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.GetApproved();

        return Result<IEnumerable<TransactionEntity>>.Success(result);
    }
}

using Bank.Transaction.Application.Abstraction.Persistence;
using Bank.Transaction.Domain.Entities;
using Bank.Transaction.Domain.Shared;
using Mediator;

namespace Bank.Transaction.Application.Queries.GetUnapproved;

internal class GetUnapprovedTransactionQueryHandler(
    ITransactionRepository transactionRepository) : 
    IRequestHandler<GetUnapprovedTransactionQuery, Result<IEnumerable<TransactionEntity>>>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async ValueTask<Result<IEnumerable<TransactionEntity>>> Handle(
        GetUnapprovedTransactionQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.GetUnapproved();

        return Result<IEnumerable<TransactionEntity>>.Success(result);
    }
}

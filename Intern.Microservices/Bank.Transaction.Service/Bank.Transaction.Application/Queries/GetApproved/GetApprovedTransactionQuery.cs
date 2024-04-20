using Bank.Transaction.Domain.Entities;
using Bank.Transaction.Domain.Shared;
using Mediator;

namespace Bank.Transaction.Application.Queries.GetApproved;

public sealed record GetApprovedTransactionQuery() : IRequest<Result<IEnumerable<TransactionEntity>>>;

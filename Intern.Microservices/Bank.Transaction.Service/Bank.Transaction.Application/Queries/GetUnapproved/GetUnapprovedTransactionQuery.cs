using Bank.Transaction.Domain.Entities;
using Bank.Transaction.Domain.Shared;
using Mediator;

namespace Bank.Transaction.Application.Queries.GetUnapproved;

public sealed record GetUnapprovedTransactionQuery() : IRequest<Result<IEnumerable<TransactionEntity>>>;

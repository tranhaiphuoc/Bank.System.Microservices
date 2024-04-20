using Bank.Transaction.Api.Requests;
using Bank.Transaction.Api.Responses;
using Bank.Transaction.Application.Commands.BuyPaymentApproval;
using Bank.Transaction.Application.Commands.DepositApproval;
using Bank.Transaction.Application.Commands.SellPaymentApproval;
using Bank.Transaction.Application.Queries.GetApproved;
using Bank.Transaction.Application.Queries.GetUnapproved;
using Bank.Transaction.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Transaction.Api.Controllers.V1;

/// <summary>
/// </summary>
/// <response code="500">An error has occured</response>
[Route("[controller]")]
public class TransactionsController(ISender sender) : ApiController
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Get a list of approved transactions
    /// </summary>
    /// <returns>A list of approved transactions</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Transactions/Approved
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    /// </remarks>
    /// <response code="200">Either if the list has any or not</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("Approved")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<IEnumerable<TransactionEntity>>))]
    public async Task<IActionResult> GetApprovedTransaction()
    {
        var result = await _sender.Send(new GetApprovedTransactionQuery());

        return HandleError(result);
    }

    /// <summary>
    /// Get a list of unapproved transactions
    /// </summary>
    /// <returns>A list of unapproved transactions</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Transactions/Unapproved
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    /// </remarks>
    /// <response code="200">Either if the list has any or not</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("Unapproved")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<IEnumerable<TransactionEntity>>))]
    public async Task<IActionResult> GetUnapprovedTransaction()
    {
        var result = await _sender.Send(new GetUnapprovedTransactionQuery());

        return HandleError(result);
    }

    /// <summary>
    /// Approved a deposit transaction
    /// </summary>
    /// <returns>Error code for the act of appoving a transaction</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Transactions/Approval
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    ///     
    ///     Body:
    ///     {
    ///         "transactionId": "00000000-0000-0000-0000-000000000001",
    ///         "status": 1,
    ///         "approvedBy": "000000000001"
    ///     }
    /// </remarks>
    /// <response code="200">Success approving the transaction</response>
    /// <response code="404">No transaction was found</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("Approval/Deposit")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> DepositApproval([FromBody] ApprovalRequest request)
    {
        var result = await _sender.Send(new DepositApprovalCommand(
            request.TransactionId,
            request.Status,
            request.ApprovedBy));

        return HandleError(result);
    }

    /// <summary>
    /// Approved a buy payment transaction
    /// </summary>
    /// <returns>Error code for the act of appoving a transaction</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Transactions/Approval
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    ///     
    ///     Body:
    ///     {
    ///         "transactionId": "00000000-0000-0000-0000-000000000001",
    ///         "status": 1,
    ///         "approvedBy": "000000000001"
    ///     }
    /// </remarks>
    /// <response code="200">Success approving the transaction</response>
    /// <response code="404">No transaction was found</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("Approval/Buy")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> BuyPaymentApproval([FromBody] ApprovalRequest request)
    {
        var result = await _sender.Send(new BuyPaymentApprovalCommand(
            request.TransactionId,
            request.Status,
            request.ApprovedBy));

        return HandleError(result);
    }

    /// <summary>
    /// Approved a sell payment transaction
    /// </summary>
    /// <returns>Error code for the act of appoving a transaction</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Transactions/Approval/Sell
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    ///     
    ///     Body:
    ///     {
    ///         "transactionId": "00000000-0000-0000-0000-000000000001",
    ///         "status": 1,
    ///         "approvedBy": "000000000001"
    ///     }
    /// </remarks>
    /// <response code="200">Success approving the transaction</response>
    /// <response code="404">No transaction was found</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("Approval/Sell")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> SellPaymentApproval([FromBody] ApprovalRequest request)
    {
        var result = await _sender.Send(new SellPaymentApprovalCommand(
            request.TransactionId,
            request.Status,
            request.ApprovedBy));

        return HandleError(result);
    }
}

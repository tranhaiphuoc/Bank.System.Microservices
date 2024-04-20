using Bank.Balance.Api.Requests;
using Bank.Balance.Api.Responses;
using Bank.Balance.Application.Commands.BuyPayment;
using Bank.Balance.Application.Commands.DepositMoney;
using Bank.Balance.Application.Commands.HoldAmount;
using Bank.Balance.Application.Commands.SellPayment;
using Bank.Balance.Application.Commands.UnholdAmount;
using Bank.Balance.Application.Queries.GetBalance;
using Bank.Balance.Application.Queries.IsBalanceValid;
using Bank.Balance.Domain.ValueObjects;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Balance.Api.Controllers.V1;

/// <summary>
/// </summary>
/// <response code="500">A server error has occured</response>
[Route("[controller]")]
public class BalancesController(ISender sender) : ApiController
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Get balance of an account
    /// </summary>
    /// <returns>Balance information if possible</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Balances?IdCard=000000000001&amp;AccountNumber=0000000001
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    /// </remarks>
    /// <response code="200">Success getting account's balance</response>
    /// <response code="400">Provided parameters were not valid</response>
    /// <response code="404">No balance was found</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<AccountBalance>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> GetBalance([FromQuery] GetBalanceRequest request)
    {
        var result = await _sender.Send(
            new GetBalanceQuery(request.IdCard, request.AccountNumber));

        if (result.IsSuccess is false)
        {
            var errorResult = await _sender.Send(
                new IsBalanceValidQuery(request.IdCard, request.AccountNumber));

            return HandleError(errorResult);
        }

        return HandleErrorWithData(result);
    }

    /// <summary>
    /// Deposit money to an account
    /// </summary>
    /// <returns>Corresponded status code</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Balances/Deposit
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    ///     
    ///     Body:
    ///     {
    ///         "accountNumber": "0000000001",
    ///         "amount": 1000,
    ///         "description": "sample description"
    ///     {
    /// </remarks>
    /// <response code="200">Deposit successfully</response>
    /// <response code="404">No balance was found</response>
    [Authorize]
    [HttpPost("Deposit")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> DepositMoney([FromBody] DepositMoneyRequest request)
    {
        var result = await _sender.Send(
            new DepositMoneyCommand(
                request.AccountNumber,
                request.Amount,
                request.Description));

        return HandleError(result);
    }

    /// <summary>
    /// Hold money from usable amount
    /// </summary>
    /// <returns>Account's balance after holding the money if possible</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Balances/Hold
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    ///     
    ///     Body:
    ///     {
    ///         "idCard": "000000000001",
    ///         "accountNumber": "0000000001",
    ///         "amount": 1000,
    ///         "description": "sample description"
    ///     {
    /// </remarks>
    /// <response code="200">Hold money successfully</response>
    /// <response code="400">Provided parameters were not valid</response>
    /// <response code="404">No balance was found</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("Hold")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> HoldAmount([FromBody] HoldAmountRequest request)
    {
        var result = await _sender.Send(
            new HoldAmountCommand(
                request.IdCard,
                request.AccountNumber,
                request.Amount,
                request.Description));

        return HandleError(result);
    }

    /// <summary>
    /// Unhold money from hold amount
    /// </summary>
    /// <returns>Account's balance after unholding the money if possible</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Balances/Unhold
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    /// 
    ///     Body:
    ///     {
    ///         "idCard": "000000000001",
    ///         "accountNumber": "0000000001",
    ///         "amount": 1000,
    ///         "description": "sample description"
    ///     {
    /// </remarks>
    /// <response code="200">Unhold money successfully</response>
    /// <response code="400">Provided parameters were not valid</response>
    /// <response code="404">No balance was found</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("Unhold")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> UnholdAmount([FromBody] UnholdAmountRequest request)
    {
        var result = await _sender.Send(
            new UnholdAmountCommand(
                request.IdCard,
                request.AccountNumber,
                request.Amount,
                request.Description));

        return HandleError(result);
    }

    /// <summary>
    /// Buy stocks from a securities' account
    /// </summary>
    /// <returns>Account's balance after the buying process if successful</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Balances/Buy
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    /// 
    ///     Body:
    ///     {
    ///         "idCard": "123456789",
    ///         "accountNumber": "0983639790",
    ///         "securitiesIdCard": "987654321",
    ///         "securitiesAccount": "FPT6868686",
    ///         "amount": 2000000,
    ///         "description": "Thanh toan tien mua 200 ACB ngay 2023-12-29"
    ///     }
    /// </remarks>
    /// <response code="200">Successful buy payment process</response>
    /// <response code="400">Passed parameters were not valid</response>
    /// <response code="404">No account was found</response>
    [Authorize]
    [HttpPost("Buy")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<AccountBalance>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> BuyPayment([FromBody] BuyPaymentRequest request)
    {
        var result = await _sender.Send(
            new BuyPaymentCommand(
                request.IdCard,
                request.AccountNumber,
                request.SecuritiesIdCard,
                request.SecuritiesAccount,
                request.Amount,
                request.Description));

        if (result.IsSuccess)
        {
            return HandleErrorWithData(result);
        }

        return HandleError(result);
    }

    /// <summary>
    /// Sell stocks to a securities' account
    /// </summary>
    /// <returns>Account's balance after the selling process if successful</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Balances/Sell
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    ///     
    ///     Body:
    ///     {
    ///         "idCard": "123456789",
    ///         "accountNumber": "0983639790",
    ///         "securitiesIdCard": "987654321",
    ///         "securitiesAccount": "FPT6868686",
    ///         "amount": 2000000,
    ///         "description": "Nhan tien ban 200 ACB ngay 2023-12-29"
    ///     }
    /// </remarks>
    /// <response code="200">Successful sell payment process</response>
    /// <response code="400">Passed parameters were not valid</response>
    /// <response code="404">No account was found</response>
    [Authorize]
    [HttpPost("Sell")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<AccountBalance>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> SellPayment([FromBody] SellPaymentRequest request)
    {
        var result = await _sender.Send(
            new SellPaymentCommand(
                request.IdCard,
                request.AccountNumber,
                request.SecuritiesIdCard,
                request.SecuritiesAccount,
                request.Amount,
                request.Description));

        if (result.IsSuccess)
        {
            return HandleErrorWithData(result);
        }

        return HandleError(result);
    }
}

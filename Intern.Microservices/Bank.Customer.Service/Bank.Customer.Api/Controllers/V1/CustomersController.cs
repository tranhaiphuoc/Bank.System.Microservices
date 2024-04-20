using Bank.Customer.Api.Requests;
using Bank.Customer.Api.Responses;
using Bank.Customer.Application.Commands.OpenCustomer;
using Bank.Customer.Application.Queries.GetCustomerAccount;
using Bank.Customer.Application.Queries.IsCustomerAccountValid;
using Bank.Customer.Domain.ValueObjects;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Customer.Api.Controllers.V1;

/// <summary>
/// </summary>
/// <response code="500">A server error has occured</response>
[Route("[controller]")]
public class CustomersController(ISender sender) : ApiController
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Return the information of an account.
    /// </summary>
    /// <returns>An account has the provided accountNumber and idCard if possible</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Customers?idCard=000000000001&amp;accountNumber=0000000001
    ///     
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    /// </remarks>
    /// <response code="200">Success retrieving the account</response>
    /// <response code="400">Provided parameters were not valid</response>
    /// <response code="404">No account was found</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<CustomerAccount>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseObject))]
    public async Task<IActionResult> GetCustomerAccount(
       [FromQuery] GetCustomerAccountRequest request)
    {
        var result = await _sender.Send(new GetCustomerAccountQuery(
            request.AccountNumber,
            request.IdCard));

        if (result.IsSuccess is false)
        {
            var errorResult = await _sender.Send(new IsCustomerAccountValidQuery(
                request.AccountNumber,
                request.IdCard));

            return HandleError(errorResult);
        }

        return HandleErrorWithData(result);
    }

    /// <summary>
    /// Open a new customer.
    /// </summary>
    /// <returns>A response after opening a new customer</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Customers/Open
    ///     
    ///     Body:
    ///     {
    ///         "idCard": "000000000001",
    ///         "name": "Tran Van A",
    ///         "dateOfBirth": "2000-01-01",
    ///         "phoneNumber": "090000001",
    ///         "address": "Hell",
    ///         "idCardDate": "2024-01-16",
    ///         "idCardPlace": "Khanh Hoa",
    ///         "accountNumber": "0000000001",
    ///         "accountType": 1,
    ///         "password": "123456"
    ///     }
    /// </remarks>
    /// <response code="200">Opened a new customer successfully</response>
    /// <response code="400">Provided parameters were not valid</response>
    [HttpPost("Open")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    public async Task<IActionResult> OpenCustomer(
        [FromBody] OpenCustomerRequest request)
    {
        var result = await _sender.Send(new OpenCustomerCommand(
            request.IdCard,
            request.Name,
            request.DateOfBirth,
            request.PhoneNumber,
            request.Address,
            request.IdCardDate,
            request.IdCardPlace,
            request.AccountNumber,
            request.AccountType,
            request.Password));

        return HandleError(result);
    }
}

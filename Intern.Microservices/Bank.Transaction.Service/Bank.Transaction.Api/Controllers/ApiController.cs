using Bank.Transaction.Api.Responses;
using Bank.Transaction.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Bank.Transaction.Api.Controllers;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseObject))]
public class ApiController : ControllerBase
{
    protected IActionResult HandleError(Result result)
    {
        return result switch
        {
            _ => StatusCode(
                result.Error.StatusCode,
                new ResponseObject(result.Error.Code, result.Error.Message))
        };
    }

    protected IActionResult HandleError<T>(Result<T> result) where T : class
    {
        return StatusCode(
            result.Error.StatusCode,
            new ResponseDataObject<T>(
                result.Error.Code,
                result.Error.Message,
                result.Value));
    }
}

using Auth.Api.Contracts.Responses;
using Auth.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Auth.Api.Controllers;

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
            IValidationResult validationResult => StatusCode(
                result.Error.StatusCode,
                new ResponseValidationObject(result.Error.Code, result.Error.Message, validationResult.Errors)),
            _ => StatusCode(
                result.Error.StatusCode,
                new ResponseObject(result.Error.Code, result.Error.Message))
        };
    }

    protected IActionResult HandleErrorWithData<T>(Result<T> result)
    {
        return StatusCode(
            result.Error.StatusCode,
            new ResponseDataObject<T>(
                result.Error.Code,
                result.Error.Message,
                result.Value));
    }
}

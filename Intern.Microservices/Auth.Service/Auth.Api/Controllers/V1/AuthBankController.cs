using Auth.Api.Contracts.Requests.Bank;
using Auth.Api.Contracts.Responses;
using Auth.Application.Bank.Commands.RefreshToken;
using Auth.Application.Bank.Commands.RevokeRefreshToken;
using Auth.Application.Bank.Queries.Login;
using Auth.Application.Contracts;
using Auth.Domain.ValueObjects;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.V1;

/// <summary>
/// </summary>
/// <response code="500">An error has occured</response>
[Route("Auth/Bank")]
public class AuthBankController(ISender sender) : ApiController
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Log in.
    /// </summary>
    /// <returns>Token for authentication</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Auth/Bank/Login
    ///     Body:
    ///     {
    ///         "username": "000000000001",
    ///         "password": "123"
    ///     }
    /// </remarks>
    /// <response code="200">Log in successfully</response>
    /// <response code="400">Wrong username or password</response>
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<LoginResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    public async Task<IActionResult> Login(
        [FromBody] BankLoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new BankLoginQuery(request.Username, request.Password), cancellationToken);

        if (result.IsSuccess is false)
        {
            return HandleError(result);
        }

        return HandleErrorWithData(result);
    }


    /// <summary>
    /// Refresh an expired JWT token.
    /// </summary>
    /// <returns>A newly generated JWT token and old refresh token</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Auth/Bank/Refresh
    ///     Body:
    ///     {
    ///         "accessToken": "&lt;access_token>",
    ///         "refreshToken": "&lt;refresh_token>"
    ///     }
    /// </remarks>
    /// <response code="200">Generating a new JWT token successfuly</response>
    /// <response code="400">Access token has not expired</response>
    /// <response code="401">Invalid token</response>
    [HttpPost("Refresh")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDataObject<TokenPair>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseObject))]
    public async Task<IActionResult> RefreshToken([FromBody] BankRefreshTokenRequest request)
    {
        var result = await _sender.Send(
            new BankRefreshTokenCommand(request.AccessToken, request.RefreshToken));

        if (result.IsSuccess is false)
        {
            return HandleError(result);
        }

        return HandleErrorWithData(result);
    }


    /// <summary>
    /// Revoke refresh token of an user
    /// </summary>
    /// <returns>An error code after revoking a token</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Auth/Bank/Logout
    ///     Header:
    ///     {
    ///         "Authorization": "Bearer &lt;access_token>"
    ///     }
    /// 
    ///     Body:
    ///     {
    ///         "refreshToken": "&lt;refresh_token>"
    ///     }
    /// </remarks>
    /// <response code="200">Revoking user's token successfully</response>
    /// <response code="401">User has not logged in yet</response>
    [Authorize]
    [HttpPost("Revoke")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseObject))]
    public async Task<IActionResult> RevokeRefreshToken(
        [FromBody] BankRevokeRefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var jwtToken = Request.Headers.Authorization.ToString().Split(" ").Last();

        var result = await _sender.Send(new BankRevokeRefreshTokenCommand(
            jwtToken!,
            request.RefreshToken), cancellationToken);

        return HandleError(result);
    }
}

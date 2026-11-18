using DotnetBackend.Api.Common.Responses;
using Microsoft.AspNetCore.Diagnostics;
using DotnetBackend.Application.Common.Exceptions;
namespace DotnetBackend.Api.Common.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        var statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;

        httpContext.Response.ContentType = "application/json";

        var message = statusCode == StatusCodes.Status500InternalServerError
            ? "An unexpected error occurred."
            : exception.Message;

        var _response = ApiResponse<object>.Fail(message);

        await httpContext.Response.WriteAsJsonAsync(
            _response,
            cancellationToken
        );
        return true;
    }
}
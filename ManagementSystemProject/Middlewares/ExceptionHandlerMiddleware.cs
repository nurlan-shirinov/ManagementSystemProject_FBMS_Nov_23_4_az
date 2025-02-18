using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses;
using System.Net;
using System.Text.Json;

namespace ManagementSystemProject.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            switch (error)
            {
                case BadRequestException:
                    var message = new List<string>(){ error.Message };
                    await WriteError(context, HttpStatusCode.BadRequest, message);
                    break;

                default:
                    break;
            }
        }
    }

    public static async Task WriteError(HttpContext context, HttpStatusCode statusCode, List<string> messages)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(new Result(messages));
        await context.Response.WriteAsync(json);
    }


}
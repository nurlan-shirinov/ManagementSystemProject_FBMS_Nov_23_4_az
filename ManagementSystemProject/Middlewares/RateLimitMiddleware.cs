using ManagementSystem.Common.Exceptions;
using System.Collections.Concurrent;

namespace ManagementSystemProject.Middlewares;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _requestLimit;
    private readonly TimeSpan _timeSpan;
    private readonly ConcurrentDictionary<string, List<DateTime>> _requestTimes = new(); // Ip addresler uzre reqeustler
    private readonly IHttpContextAccessor _contextAccessor;


    public RateLimitMiddleware(RequestDelegate next, int requestLimit, TimeSpan timeSpan, IHttpContextAccessor contextAccessor)
    {
        _next = next;
        _requestLimit = requestLimit;
        _timeSpan = timeSpan;
        _contextAccessor = contextAccessor;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        var isAuthenticated = _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        if (!isAuthenticated)
        {
            var clientId = _contextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString(); // Mushterinin IP addresin aliriq
            var now = DateTime.UtcNow;
            var requesLog = _requestTimes.GetOrAdd(clientId, new List<DateTime>());
            lock (requesLog)
            {
                requesLog.RemoveAll(timeStamp => timeStamp <= now - _timeSpan);
                if (requesLog.Count >= _requestLimit)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.Headers.RetryAfter = _timeSpan.TotalSeconds.ToString();
                    return;

                }
                requesLog.Add(now);
            };
            await _next(context);
        }
        await _next(context);
    }

}
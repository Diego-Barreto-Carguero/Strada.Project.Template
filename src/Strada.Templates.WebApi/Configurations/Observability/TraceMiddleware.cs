// <copyright file="TraceMiddleware.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Serilog.Context;

namespace Strada.Template.Api.Configurations.Observability;

public class TraceMiddleware
{
    private readonly RequestDelegate _next;

    public TraceMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.Any(c => c.Key.Equals("x-correlation-id")))
        {
            context.Request.Headers.Add("x-correlation-id", Guid.NewGuid().ToString());
        }

        using (LogContext.PushProperty("CorrelationId", context.Request.Headers.SingleOrDefault(c => c.Key.Equals("x-correlation-id")).Value))
        using (LogContext.PushProperty("Authorization", context.Request.Headers.SingleOrDefault(c => c.Key.Equals("Authorization")).Value))
        {
            await _next(context);
        }
    }
}

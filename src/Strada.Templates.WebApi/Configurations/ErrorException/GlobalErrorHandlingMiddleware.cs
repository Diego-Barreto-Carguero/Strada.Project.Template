// <copyright file="GlobalErrorHandlingMiddleware.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Serilog;
using Strada.Template.Api.Configurations.ErrorException.Custom;

namespace Strada.Template.Api.Configurations.ErrorException;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public GlobalErrorHandlingMiddleware(RequestDelegate requestDelegate)
        => _requestDelegate = requestDelegate;

    public async Task Invoke(HttpContext httpContext)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        var response = httpContext.Response;
        response.ContentType = "application/json";

        try
        {
            await _requestDelegate(httpContext);
        }
        catch (BaseCustomException baseCustomException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Type = baseCustomException.GetType().Name;
            problemDetails.Title = "The request could not be understood by the server due to incorrect syntax.";
            problemDetails.Detail = baseCustomException.Message;

            response.StatusCode = StatusCodes.Status400BadRequest;

            Log.Logger.Error(baseCustomException.Message);
        }
        catch (Exception exception)
        {
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Type = exception.GetType().Name;
            problemDetails.Title = "The server encountered an unexpected condition that prevented it from fulfilling the request.";
            problemDetails.Detail = exception.Message;

            response.StatusCode = StatusCodes.Status500InternalServerError;

            Log.Logger.Error(exception.Message);
        }

        await response.WriteAsJsonAsync(problemDetails);
    }
}

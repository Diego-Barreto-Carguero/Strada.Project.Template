// <copyright file="CustomAuthorizationFilter.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Strada.Template.Api.Configurations.ErrorException;

public class CustomAuthorizationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
{
    public CustomAuthorizationFilter()
    {
        Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    }

    public AuthorizationPolicy Policy { get; }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.Filters.Any(item => item is IAllowAnonymousFilter)) return;

        var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
        var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
        var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

        if (authorizeResult.Challenged)
        {
            context.Result = new JsonResult(new ProblemDetails
            {
                Title = "Unauthorized Access",
                Detail = "The request was not applied because it does not have valid authentication credentials for the target resource.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7235",
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status401Unauthorized
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        if (authorizeResult.Forbidden)
        {
            context.Result = new JsonResult(new ProblemDetails
            {
                Title = "Unauthorized request. The client does not have access rights to the content.",
                Detail = "The server understood the request, but refuses to authorize it, as it considers it insufficient to grant access.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231",
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status403Forbidden
            })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }

        if (!context.HttpContext.Response.Headers.Any(c => c.Key.Equals("WWW-Authenticate")))
        {
            context.HttpContext.Response.Headers.Add("WWW-Authenticate", string.Empty);
        }
    }
}

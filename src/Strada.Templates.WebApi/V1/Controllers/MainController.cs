// <copyright file="MainController.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Strada.Templates.WebApi.Configurations.ErrorException;
using Swashbuckle.AspNetCore.Annotations;

namespace Strada.Templates.WebApi.V1.Controllers
{
    [CustomAuthorizationFilter]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request was not applied because it does not have valid authentication credentials for the target resource.")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "The server understood the request, but refuses to authorize it, as it considers it insufficient to grant access.")]
    public abstract class MainController : ControllerBase
    {
        protected readonly ILogger<MainController> logger;

        protected MainController(ILogger<MainController> logger) => this.logger = logger;
    }
}

// <copyright file="ModelStateErrorAttribute.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Serilog;

namespace Strada.Template.Api.Configurations.ErrorException;

public class ModelStateErrorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new JsonResult(new ValidationProblemDetails(context.ModelState)
            {
                Title = "The request could not be understood by the server due to incorrect syntax.",
                Detail = "One or more validation errors occurred.",
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status400BadRequest,
                Type = "ModelStateValidation"
            })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            Log.Logger.Error(JsonConvert.SerializeObject(context.Result));
        }
    }
}

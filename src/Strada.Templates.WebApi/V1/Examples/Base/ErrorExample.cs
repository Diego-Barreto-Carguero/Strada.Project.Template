// <copyright file="ErrorExample.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Strada.Templates.WebApi.V1.Models.Base;
using Swashbuckle.AspNetCore.Filters;

namespace Strada.Templates.WebApi.V1.Examples.Base;

public record ErrorExample : IExamplesProvider<ErrorResponse>
{
    public ErrorResponse GetExamples()
    {
        return new ErrorResponse()
        {
            ErrorResult = new string[] { "Error Message" },
            CorrelationId = "3be3d244-1fa5-41a9-89db-787c9b05096b",
            Success = false,
            TraceId = "0HMR6GT785PTP:00000004"
        };
    }
}

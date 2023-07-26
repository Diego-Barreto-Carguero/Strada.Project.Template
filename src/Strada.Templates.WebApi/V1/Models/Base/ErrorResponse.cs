// <copyright file="ErrorResponse.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

namespace Strada.Template.WebApi.V1.Models.Base;

public record ErrorResponse
{
    public IEnumerable<string> ErrorResult { get; set; }

    public string TraceId { get; set; }

    public bool Success { get; set; }

    public string CorrelationId { get; set; }
}

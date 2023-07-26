// <copyright file="RedocDocumentAttribute.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Strada.Template.WebApi.Configurations.Redoc;

public class RedocDocumentAttribute : IDocumentFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RedocDocumentAttribute(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.ExternalDocs = new OpenApiExternalDocs
        {
            Description = $"Strada Documention v{swaggerDoc.Info.Version}",
            Url = new Uri($"{_httpContextAccessor?.HttpContext?.Request.Scheme}://{_httpContextAccessor?.HttpContext?.Request.Host}/api-docs/{context.DocumentName}")
        };
    }
}

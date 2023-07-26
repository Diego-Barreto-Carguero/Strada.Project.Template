// <copyright file="SwaggerExtensions.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Strada.Templates.WebApi.Configurations.Redoc;
using Strada.Templates.WebApi.Configurations.Swagger;
using Swashbuckle.AspNetCore.Filters;

namespace Strada.Templates.WebApi.Configurations.Swagger;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(configuration["Authentication:AccApi"])
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[]
                    {
                        "public_api"
                    }
                }
            });

            options.DocumentFilter<RedocDocumentAttribute>();
            options.EnableAnnotations();
            options.ExampleFilters();
            options.SchemaFilter<EnumSchemaFilter>();
        });
    }

    public static void UseSwaggerUI(this WebApplication webApplication)
    {
        var apiVersionDescriptionProvider = webApplication.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        webApplication.UseSwagger();

        webApplication.UseSwaggerUI(options =>
        {
            foreach (var groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(s => s.GroupName))
            {
                options.SwaggerEndpoint($"{groupName}/swagger.json", groupName.ToUpperInvariant());
                options.InjectStylesheet("/wwwroot/css/swagger-custom.css");
            }
        });
    }
}

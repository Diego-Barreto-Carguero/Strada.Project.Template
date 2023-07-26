// <copyright file="SwaggerConfiguration.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Strada.Templates.WebApi.Configurations.Swagger;

public class SwaggerConfiguration : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerConfiguration(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }

    public void Configure(string name, SwaggerGenOptions options) => Configure(options);

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
    {
        var openApiInfo = new OpenApiInfo()
        {
            Title = Assembly.GetExecutingAssembly().GetName()?.Name?.ToUpper(),
            Version = apiVersionDescription.ApiVersion.ToString(),
            Contact = new OpenApiContact
            {
                Name = "Strada",
                Email = "atendimento@strada.log.br",
                Url = new Uri("https://carguero.com.br")
            },
            Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    {
                        "x-logo", new OpenApiObject
                        {
                            {
                                "url", new OpenApiString("/wwwroot/images/logo_strada.png")
                            }
                        }
                    }
                }
        };

        if (apiVersionDescription.IsDeprecated)
        {
            openApiInfo.Description += "Esta versão da API foi preterida. Use uma das novas APIs disponíveis no explorer.";
        }

        return openApiInfo;
    }
}

// <copyright file="RedocExtentions.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Strada.Template.Api.Configurations.Redoc;

public static class RedocExtentions
{
    public static void UseReDocPage(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            app.UseReDoc(options =>
            {
                options.DocumentTitle = $"Strada Documention - {description.GroupName}";
                options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
                options.RoutePrefix = $"api-docs/{description.GroupName}";
                options.EnableUntrustedSpec();
                options.HideDownloadButton();
                options.ExpandResponses(string.Empty);
            });
        }
    }
}

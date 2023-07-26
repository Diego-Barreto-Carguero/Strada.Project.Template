// <copyright file="EnumSchemaFilter.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Strada.Template.WebApi.Configurations.Swagger;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;

        schema.Enum.Clear();

        Enum.GetNames(context.Type).ToList()
            .ForEach(name => schema.Enum.Add(new OpenApiString(name)));
    }
}

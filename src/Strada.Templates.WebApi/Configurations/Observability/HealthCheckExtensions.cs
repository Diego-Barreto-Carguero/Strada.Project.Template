// <copyright file="HealthCheckExtensions.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using HealthChecks.UI.Client;

namespace Strada.Template.Api.Configurations.Observability;

public static class HealthCheckExtensions
{
    public static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddElasticsearch(configuration["ElasticConfiguration:Uri"]);

        services.AddHealthChecksUI(options =>
        {
            options.AddHealthCheckEndpoint("Health Check Template API", "/healthcheck");
            options.SetEvaluationTimeInSeconds(30);
            options.MaximumHistoryEntriesPerEndpoint(60);
        }).AddInMemoryStorage();
    }

    public static void UseHealthCheck(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/healthcheck", new() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
        endpoints.MapHealthChecksUI(options =>
        {
            options.UIPath = "/dashboard";
            options.AddCustomStylesheet(@"wwwroot/css/healthcheck-ui.css");
        });
    }
}

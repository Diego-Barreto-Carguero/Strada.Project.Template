// <copyright file="LoggerExtensions.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using System.Reflection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Sinks.Elasticsearch;

namespace Strada.Template.Api.Configurations.Observability;

public static class LoggerExtensions
{
    public static void ConfigureSerilog(this IServiceCollection services)
    {
        var projectName = Assembly.GetExecutingAssembly().GetName()?.Name?.ToLower();

        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("AspNetCore.HealthChecks.UI", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithClientIp()
            .Enrich.WithClientAgent()
            .Enrich.WithCorrelationIdHeader("x-correlation-id")
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("/healthcheck")))
            .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} - {Message:lj}{NewLine}{Exception}")
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = projectName,
                ModifyConnectionSettings = x => x.BasicAuthentication(configuration["ElasticConfiguration:User"], configuration["ElasticConfiguration:Password"])
            })
            .CreateLogger();

        Log.Logger.Information("Initializing the project {project}", projectName);
    }
}

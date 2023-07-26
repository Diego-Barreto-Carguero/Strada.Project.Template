// <copyright file="Program.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Strada.Templates.WebApi.Configurations;
using Strada.Templates.WebApi.Configurations.ErrorException;
using Strada.Templates.WebApi.Configurations.Observability;
using Strada.Templates.WebApi.Configurations.Redoc;
using Strada.Templates.WebApi.Configurations.Swagger;
using Strada.Templates.WebApi.V1.Validators.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Strada.Templates.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options => options.Filters.Add(typeof(ModelStateErrorAttribute)))
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            }).ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

        builder.Services.AddJwtAuthentication();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<PersonRequestValidator>();
        builder.Services.AddFluentValidationRulesToSwagger();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiVersion();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSwagger();
        builder.Services.AddSwaggerExamplesFromAssemblyOf<IBaseExampleProvider>();
        builder.Services.AddLocalization();
        builder.Services.ConfigureOptions<SwaggerConfiguration>();
        builder.Services.AddHealthCheck(builder.Configuration);
        builder.Services.AddHeaderPropagation(s => { s.Headers.Add("x-correlation-id"); });
        builder.Services.ConfigureSerilog();
        builder.Host.UseSerilog();
        builder.Services.AddProblemDetails();

        builder.Services.AddHttpLogging(logging =>
           {
               logging.LoggingFields =
                 HttpLoggingFields.RequestPropertiesAndHeaders
               | HttpLoggingFields.RequestBody
               | HttpLoggingFields.RequestQuery
               | HttpLoggingFields.RequestMethod
               | HttpLoggingFields.RequestProtocol
               | HttpLoggingFields.RequestPath;

               logging.RequestHeaders.Add("x-correlation-id");
               logging.RequestHeaders.Add("Authorization");
           });

        var app = builder.Build();

        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        var cultures = new[] { "pt-BR", "en-US" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(cultures[0])
            .AddSupportedCultures(cultures)
            .AddSupportedUICultures(cultures);

        app.UseRequestLocalization(localizationOptions);
        app.MapControllers();
        app.UseReDocPage();
        app.UseRouting();
        app.UseHttpLogging();
        app.UseHeaderPropagation();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        app.UseMiddleware<TraceMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.UseHealthCheck();
        });

        app.UseProblemDetails();

        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            RequestPath = "/wwwroot"
        });

        app.Run();
    }
}

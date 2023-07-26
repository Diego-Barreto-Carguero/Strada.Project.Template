// <copyright file="AuthenticationExtentions.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Strada.Templates.WebApi.Configurations.Security;

namespace Strada.Templates.WebApi.Configurations;

public static class AuthenticationExtentions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var authentication = configuration.GetSection("Authentication").Get<AuthenticationModel>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = authentication.ValidateIssuer,
                ValidateAudience = authentication.ValidateAudience,
                ValidateLifetime = authentication.ValidateLifetime,
                ValidateIssuerSigningKey = authentication.ValidateIssuerSigningKey,
                ValidAudience = authentication.ValidAudience,
                ValidIssuer = authentication.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authentication.AccountsJwtSecret))
            };
        });
    }
}

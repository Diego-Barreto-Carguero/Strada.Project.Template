// <copyright file="AuthenticationModel.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

namespace Strada.Template.Api.Configurations.Security
{
    public record AuthenticationModel
    {
        public string AccountsJwtSecret { get; init; }

        public bool ValidateIssuer { get; init; }

        public bool ValidateAudience { get; init; }

        public bool ValidateLifetime { get; init; }

        public bool ValidateIssuerSigningKey { get; init; }

        public string ValidIssuer { get; init; }

        public string ValidAudience { get; init; }
    }
}

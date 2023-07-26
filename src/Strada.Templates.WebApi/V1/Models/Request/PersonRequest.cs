// <copyright file="PersonRequest.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Swashbuckle.AspNetCore.Annotations;

namespace Strada.Templates.WebApi.V1.Models.Request;

public record PersonRequest
{
    public string Name { get; init; }

    public DateTime BornIn { get; init; }

    public DocumentType DocumentType { get; init; }

    public string Document { get; init; }

    public string PhoneNumber { get; init; }

    public Address HomeAddress { get; init; }

    public Address BusinessAddress { get; init; }
}

public record Address
{
    public string AddressLine { get; init; }

    public string City { get; init; }

    public string ZipCode { get; init; }

    [SwaggerSchema(Description = "Example: Nº 23 , Nº 99786 or N/I ")]
    public string Number { get; init; }

    [SwaggerSchema(Description = "Example: House , Apartment 198 , Route 66 , KM 22")]
    public string Complement { get; init; }
}

public enum DocumentType
{
    Cpf = 1,
    Cnpj = 2,
    Passport = 3
}

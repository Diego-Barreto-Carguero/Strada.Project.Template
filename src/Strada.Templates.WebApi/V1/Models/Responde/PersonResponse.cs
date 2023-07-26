// <copyright file="PersonResponse.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Strada.Template.Api.V1.Models.Request;

namespace Strada.Template.Api.V1.Models.Responde;

public record PersonResponse
{
    public int Id { get; set; }

    public string Name { get; init; } = string.Empty;

    public DateTime BornIn { get; init; }

    public string PhoneNumber { get; init; } = string.Empty;

    public DocumentType DocumentType { get; init; }

    public string Document { get; init; } = string.Empty;

    public string Celular { get; init; } = string.Empty;

    public Address HomeAddress { get; init; }

    public Address BusinessAddress { get; init; }
}

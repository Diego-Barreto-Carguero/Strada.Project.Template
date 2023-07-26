// <copyright file="PersonRequestExample.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>
using Strada.Template.Api.V1.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Strada.Template.Api.V1.Examples.Request;

public record PersonRequestExample : IExamplesProvider<PersonRequest>
{
    public PersonRequest GetExamples()
    {
        return new PersonRequest()
        {
            Name = "Paulo Souza Silva",
            BornIn = new DateTime(1990, 10, 21),
            PhoneNumber = "11986650987",
            DocumentType = DocumentType.Cpf,
            Document = "43376795414",
            HomeAddress = new Address
            {
                City = "São Paulo",
                ZipCode = "08987909",
                AddressLine = "Rodovia Presidente castelo branco",
                Complement = "KM 187",
                Number = "S/N"
            },
            BusinessAddress = new Address
            {
                City = "São Paulo",
                ZipCode = "09876098",
                AddressLine = "Av Brigadeiro faria souza",
                Complement = "Loft 13",
                Number = "1876"
            }
        };
    }
}

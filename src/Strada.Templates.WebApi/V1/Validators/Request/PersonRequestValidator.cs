// <copyright file="PersonRequestValidator.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using FluentValidation;
using Microsoft.Extensions.Localization;
using Strada.Template.Api.V1.Models.Request;
using Strada.Template.Api.V1.Resouces;

namespace Strada.Template.Api.V1.Validators.Request;

public class PersonRequestValidator : AbstractValidator<PersonRequest>
{
    public PersonRequestValidator(IStringLocalizer<PersonMessageResource> personResourceMessages)
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value)
            .Length(8, 15)
            .WithMessage(personResourceMessages["SizeBetween"].Value);

        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value)
            .Length(8)
            .WithMessage(personResourceMessages["SizeEqualTo"].Value);

        RuleFor(x => x.Document)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value);

        RuleFor(x => x.DocumentType)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value);

        RuleFor(x => x.HomeAddress)
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value)
            .SetValidator(new PersonAddressValidator(personResourceMessages));

        RuleFor(x => x.BornIn)
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value)
            .Must(x => x <= DateTime.UtcNow)
            .WithMessage(personResourceMessages["OverEighteenYearsOld"].Value);
    }
}

public class PersonAddressValidator : AbstractValidator<Address>
{
    public PersonAddressValidator(IStringLocalizer<PersonMessageResource> personResourceMessages)
    {
        RuleFor(x => x.AddressLine)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value)
            .Length(2, 50)
            .WithMessage(personResourceMessages["SizeBetween"].Value);

        RuleFor(x => x.ZipCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value)
            .Length(8)
            .WithMessage(personResourceMessages["SizeEqualTo"].Value);

        RuleFor(x => x.City)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value)
            .Length(2, 80)
            .WithMessage(personResourceMessages["SizeBetween"].Value);

        RuleFor(x => x.Number)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage(personResourceMessages["NotNullOrEmpty"].Value);
    }
}

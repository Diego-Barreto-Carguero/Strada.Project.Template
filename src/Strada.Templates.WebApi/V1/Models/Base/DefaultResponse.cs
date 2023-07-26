// <copyright file="DefaultResponse.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

namespace Strada.Template.WebApi.V1.Models.Base;

public record DefaultResponse<TModel>
    where TModel : class
{
    public TModel Result { get; set; }
}

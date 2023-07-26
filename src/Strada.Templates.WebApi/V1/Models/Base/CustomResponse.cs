// <copyright file="CustomResponse.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

usingStrada.Template.WebApi.Configurations.Pagination;

namespace Strada.Template.WebApi.V1.Models.Base;

public record CustomResponse<TModel> : DefaultResponse<TModel>
    where TModel : class
{
    public PaginationMetadata Metadata { get; set; }
}

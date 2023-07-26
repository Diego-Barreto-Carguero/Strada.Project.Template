// <copyright file="CustomResponse.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Strada.Templates.WebApi.Configurations.Pagination;

namespace Strada.Templates.WebApi.V1.Models.Base;

public record CustomResponse<TModel> : DefaultResponse<TModel>
    where TModel : class
{
    public PaginationMetadata Metadata { get; set; }
}

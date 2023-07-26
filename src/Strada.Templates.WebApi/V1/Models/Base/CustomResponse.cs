// <copyright file="CustomResponse.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Strada.Template.Api.Configurations.Pagination;

namespace Strada.Template.Api.V1.Models.Base;

public record CustomResponse<TModel> : DefaultResponse<TModel>
    where TModel : class
{
    public PaginationMetadata Metadata { get; set; }
}

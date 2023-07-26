// <copyright file="PaginationParams.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

using Swashbuckle.AspNetCore.Annotations;

namespace Strada.Template.Api.Configurations.Pagination;

public record PaginationParams
{
    private const int _maxItemsPerPage = 50;

    private int _itemsPerPage = _maxItemsPerPage;

    public int CurrentPage { get; set; } = 1;

    [SwaggerSchema(Description = "The service will limit pagination to 50 items per page")]
    public int ItemsPerPage
    {
        get => _itemsPerPage;
        set => _itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
    }
}

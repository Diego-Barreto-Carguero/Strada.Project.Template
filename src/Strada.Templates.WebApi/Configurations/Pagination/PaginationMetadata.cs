// <copyright file="PaginationMetadata.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

namespace Strada.Template.WebApi.Configurations.Pagination;

public record PaginationMetadata
{
    public PaginationMetadata(int totalItems, int currentPage, int itemsPerPage)
    {
        TotalItems = totalItems;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
    }

    public int CurrentPage { get; private set; }

    public int TotalItems { get; private set; }

    public int TotalPages { get; private set; }

    public bool HasPreview => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;
}

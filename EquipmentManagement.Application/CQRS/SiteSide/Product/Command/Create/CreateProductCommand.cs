﻿using Microsoft.AspNetCore.Http;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command;

public class CreateProductCommand : IRequest<bool>
{
    #region properties

    public ulong? PlaceId { get; set; }

    public ulong? CategoryId { get; set; }

    public string? ProductTitle { get; set; }

    public string? RepositoryCode { get; set; }

    public string? BarCode { get; set; }

    public int EntityCount { get; set; }

    public string? Description { get; set; }

    public ulong UserId { get; set; }

    public string? BrandName { get; set; }

    public IFormFile? InvoiceImage { get; set; }

    public string? InvoiceDateTime { get; set; }

    public ulong? InvoiceNumber { get; set; }

    #endregion
}
